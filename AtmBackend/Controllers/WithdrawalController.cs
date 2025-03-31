using AtmBackend.Models.Requests;
using AtmBackend.Models.Responses;
using AtmBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AtmBackend.Controllers;

[Route("api/withdrawal")]
public class WithdrawalController(IWithdrawalService withdrawalService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<DenominationResponse>>> Withdraw([FromBody] WithdrawalRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await withdrawalService.Withdraw(request);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}
