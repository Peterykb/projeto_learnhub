using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Models;

namespace Projeto.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ComentariosController : ControllerBase
  {
    public readonly Context context;
    public ComentariosController(Context _context)
    {
      context = _context;
    }
    

  }
}
