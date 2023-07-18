using System;
using System.Collections.Generic;

namespace MVC_CRUD.Models;

public partial class Provincium
{
    public string IdProvincia { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string IdDepartamento { get; set; } = null!;
}
