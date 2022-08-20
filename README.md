# TokenManagment
.Net Core 6.0 TokenManagment Library

## Documantation

### Using

```csharp
...
using NiafixAuthentication;

namespace Example.Controllers
{
    [ApiController]
    [Route("/api/authentication")]
    public class ExampleController : ControllerBase
    {
        [HttpPost]
        public IActionResult login(ExampleModel, UserData)
        {
            //if user exist
            return Ok(TokenManagment.CreateToken(UserData));
        }

        [HttpGet]
        [Authorization]
        public IActionResult ControlToken()
        {
            return Ok(TokenManagment.TokenDecoder(Request.Headers["Authorization"]));

        }
    }
}
```