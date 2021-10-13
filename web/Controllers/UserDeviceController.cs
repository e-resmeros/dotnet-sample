using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Core;
using api.Core.Model;
using api.Core.Utilities;
using api.Data;
using api.Models;
using api.Models.Auth;
using api.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Route("userDevice")]
    public class UserDeviceController : BaseWebController<UserDeviceDTO>
    {
        /// Holds the repository injected into the controller.
        private readonly IUserDeviceRepoWrapper _repository;

        private readonly IOptions<JwtTokenConfig> _tokenConfig;

        public UserDeviceController(IOptions<JwtTokenConfig> tokenConfig, IUserDeviceRepoWrapper repository, IMapper mapper, ILoggerManager logger)
            : base(mapper, logger)
        {
            _repository = repository;
            _tokenConfig = tokenConfig;
        }

        /// <summary>This function gets called on GET [baseUri]/userDevice/userDeviceIndex.
        /// Get the "admin" user with the given user ID.
        /// If there are no users found, redirect to login page.
        /// Else, redirect to User Device List screen.</summary>
        [HttpGet]
        [ActionName("userDeviceIndex")]
        public IActionResult Index()
        {
            _logger.LogInfo($"User= {HttpContext.Session.GetString("userID")} -- START");
            try
            {
                var loggedUser = HttpContext.Session.GetString("userID");

                if (string.IsNullOrEmpty(loggedUser))
                {
                    return RedirectToAction("loginIndex", "WebLogin");
                }

                var user = _repository.User.GetAdminByUserName(new User {
                    UserName = loggedUser
                });

                if (null == user)
                {
                    return RedirectToAction("loginIndex", "WebLogin");
                }
                ViewData["empName"] = user.Name;

                return View("UserDeviceList");
            } catch (Exception e)
            {
                _logger.LogError(e.ToString());
                ViewBag.Error = "The server has encountered an unexpected error. Please try again later.";
                return View("UserDeviceList");
            } finally
            {
                _logger.LogInfo($"User= {HttpContext.Session.GetString("userID")} -- END");
            }
        }

        /// <summary>This function gets called on POST [baseUri]/userDevice/all.
        /// Given a set of values for each given parameter, find all devices that
        /// satisfy the query.</summary>
        /// <param>dtParameters - List of filter queries from client side.
        [HttpPost("all")]
        public async Task<IActionResult> GetUserDevices(DtParameters dtParameters)
        {
            _logger.LogInfo($"User= {HttpContext.Session.GetString("userID")} -- START");
            try
            {
                List<string> searchByColumns = new List<string>();
                List<string> searchByValue = new List<string>();

                foreach (DtColumn column in dtParameters.Columns)
                {
                    if (!string.IsNullOrEmpty(column.Search.Value))
                    {
                        searchByColumns.Add(column.Data);
                        searchByValue.Add(column.Search.Value.ToLower());
                    }
                }

                if (0 == searchByColumns.Count)
                {
                    searchByColumns.Add("status");
                    searchByValue.Add("a");
                }

                var orderCriteria = string.Empty;
                var orderAscendingDirection = true;

                // if order is not defined, set [orderCriteria] to the index of the record.
                if (dtParameters.Order != null)
                {
                    orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                    orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                }
                else
                {
                    orderCriteria = "Index";
                    orderAscendingDirection = true;
                }

                var result = await _repository.UserDevice.GetUserDevices();
                var totalResultsCount = result.Count();

                if (searchByColumns.Count > 0)
                {
                    // For each key with a value in [searchByColumns], filter the list using its value.
                    for (int idx = 0; idx < searchByColumns.Count; idx++)
                    {
                        switch (searchByColumns[idx])
                        {
                            case "companyName":
                                result = result.Where(r => r.CompanyName.ToLower().Contains(searchByValue[idx])).ToList();
                                break;
                            case "employeeName":
                                result = result.Where(r => r.EmployeeName.ToLower().Contains(searchByValue[idx])).ToList();
                                break;
                            case "employeeNo":
                                result = result.Where(r => r.EmployeeNo.ToLower().Contains(searchByValue[idx])).ToList();
                                break;
                            case "status":
                                if (!searchByValue[idx].ToLower().Equals("all"))
                                    result = result.Where(r => r.Status.ToLower().Equals(searchByValue[idx])).ToList();
                                break;
                        }
                    }
                }
                
                result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc).ToList()
                                                    : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc).ToList();

                var filteredResultsCount = result.Count();

                var mappedResult = _mapper.Map<IEnumerable<UserDeviceDTO>>(result);
                
                var newRes = mappedResult.Skip(dtParameters.Start)
                                .Take(dtParameters.Length)
                                .ToList();

                return Json(new {
                    data = newRes,
                    draw = dtParameters.Draw,
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount });
            } catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(500, new {
                    code = "Error",
                    message = "The server has encountered an unexpected error. Please try again later."
                });
            } finally
            {
                _logger.LogInfo($"User= {HttpContext.Session.GetString("userID")} -- END");
            }
        }

        /// <summary>This function gets called on POST [baseUri]/userDevice.
        /// Provided the user and device details, check if the device is currently
        /// linked with the user. If it is, unlink the device from the user.
        /// Else, do nothing. </summary>
        /// <param>userDevDto - Contains details of the user and the device for unlinking.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlinkDevice([FromForm] UserDeviceDTO userDevDto)
        {
            try
            {
                if (!Validate(userDevDto))
                {
                    return BadRequest(userDevDto.Error);
                }

                var userDevModel = _mapper.Map<Device>(userDevDto);
                var res = await _repository.Device.GetDeviceByUserDeviceId(userDevDto.UserDeviceID);
                
                _repository.Device.UnlinkDevice(res);

                await _repository.Device.SaveAsync();

                return RedirectToAction("userDeviceIndex");
            } catch
            {
                ViewBag.Error = "The server has encountered an unexpected error. Please try again later.";
                return View("UserDeviceList");
            }
        }

        /// <summary>This function will be called on each POST request
        /// for data sanitation and validation.</summary>
        /// <param>model - The translated model of the data passed from the API.
        public override bool Validate(UserDeviceDTO model)
        {
            return true;
        }
    }
}