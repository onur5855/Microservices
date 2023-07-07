using Microsoft.AspNetCore.Mvc;
using ServiceShared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceShared.ControllerBases
{
    public class CustumBaseController :ControllerBase
    {
        public IActionResult CreateActionResultIntance<T>(ResponseDto<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

    }
}
