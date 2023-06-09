﻿// Libraries
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using Utilitaries.DTO;
using Utilitaries.DTO.Pager;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Logs controller
    /// </summary>
    [ApiController]
    [Route("logs")]
    public class LogsController : ControllerBase
    {
        // Vars
        private ILogService LogService { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogsController(ILogService logService)
        {
            LogService = logService;
        }

        // Methods

        /// <summary>
        /// Create parking log
        /// </summary>
        /// <param name="log">Log data</param>
        /// <returns>201 - Created</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LogDTO log)
        {
            await LogService.Create(log);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Get parking logs paginated
        /// </summary>
        /// <param name="pager">Pager input data</param>
        /// <returns>200 - OK, paginated logs</returns>
        [HttpGet]
        public async Task<ActionResult<PagerOutputDTO<LogDTO>>> Get([FromQuery] PagerInputDTO pagerInput)
        {
            PagerOutputDTO<LogDTO> pagerOutput = await LogService.Get(pagerInput);
            return StatusCode(StatusCodes.Status200OK, pagerOutput);
        }

        /// <summary>
        /// Update parking log
        /// </summary>
        /// <param name="log">Log data</param>
        /// <returns>200 - OK</returns>
        [HttpPut]
        public async Task<ActionResult> Update(
            [Required(ErrorMessage = "Vehicle number plate is required")]
            [StringLength(6, MinimumLength = 6, ErrorMessage = "Vehicle number plate must have 6 characters")]
            [RegularExpression("[A-Z0-9]*", ErrorMessage = "Vehicle number plate must have only uppercase letters and/or numbers")] string numberPlate,
            [Required(ErrorMessage = "Departure date is required")] DateTime departure,
            [StringLength(8, MinimumLength = 8, ErrorMessage = "Bill discount number must have 8 numbers")]
            [RegularExpression("[0-9]*", ErrorMessage = "Bill discount number must have only numbers")] string? billDiscountNumber)
        {
            await LogService.Update(numberPlate, departure, billDiscountNumber);
            return StatusCode(StatusCodes.Status200OK, new { message = "Parking log updated successfully" });
        }
    }
}
