using System;
using System.Collections.Generic;

namespace CapaAdmin.Models;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int? IdC1iente { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public virtual Cliente? IdC1ienteNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
