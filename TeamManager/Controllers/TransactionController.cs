using Microsoft.AspNetCore.Mvc;
using TeamManager.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/Transaction")]
public class TransactionController(AppDbContext context):  ControllerBase
{
    
}