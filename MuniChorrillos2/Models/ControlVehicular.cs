using System;
using System.Collections.Generic;

namespace MuniChorrillos2.Models;

public partial class ControlVehicular
{
    public int IdControl { get; set; }

    public string? Placa { get; set; }

    public TimeSpan Hingreso { get; set; }

    public TimeSpan Hsalida { get; set; }

    public DateTime Dia { get; set; }

    public int Cochera { get; set; }

    public string? Estado { get; set; }

}
