﻿using System;
using System.Collections.Generic;

namespace CapaAdmin.Models;

public partial class Distrito
{
    public string IdDistrito { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string IdProvincia { get; set; } = null!;

    public string IdDepartamento { get; set; } = null!;
}
