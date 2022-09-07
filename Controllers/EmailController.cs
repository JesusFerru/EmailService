﻿using Microsoft.AspNetCore.Mvc;

namespace EmailApp4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;

        public EmailController(IEmailService emailService, ITemplateService templateService)
        {
            _emailService = emailService;
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            var result = await _emailService.GetEmails();
            return Ok(result);
        }


        [HttpPost(Name = "Post_Email")]
        public async Task<IActionResult> PostEmail([FromHeader] int id, [FromBody] DataEmail request)
        {
            //  _emailService.SendEmail(request);
        //    EmailTemplate template = await _templateService.GetUniqueTemplate(id);

            if (await _templateService.Verif(id) == true)
            {
                var result = await _emailService.Post(id, request);

                return Ok(result);
            }
            
            return NotFound();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DataEmail request)
        {

            var result = await _emailService.PutEmail(id, request);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _emailService.DeleteEmail(id);
            if (result != null)
            {
                return Ok(result);
            }
            else return NotFound();

        }
    }
}