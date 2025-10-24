using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

// Programa completo: Registro de asignaturas y guardado en Supabase
// Reemplaza TU_URL_SUPABASE y TU_API_KEY en el método GuardarEnSupabase

class Asignatura
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Prerrequisito { get; set; }

    public void ImprimirDiagrama()
    {
        Console.WriteLine("\n+--------------------+");
        Console.WriteLine("|     Asignatura     |");
        Console.WriteLine("+--------------------+");
        Console.WriteLine($"| Código: {Codigo}");
        Console.WriteLine($"| Nombre: {Nombre}");
        Console.WriteLine($"| Prerreq: {Prerrequisito}");
        Console.WriteLine("+--------------------+");
    }
}

class Profesor
{
    public string Nombre { get; set; }
    public string Especialidad { get; set; }
    public void ImprimirDiagrama()
    {
        Console.WriteLine("\n+--------------------+");
        Console.WriteLine("|      Profesor      |");
        Console.WriteLine("+--------------------+");
        Console.WriteLine($"| Nombre: {Nombre}");
        Console.WriteLine($"| Departamento: {Especialidad}");
        Console.WriteLine("+--------------------+");
    }
}

class Programa 
{
    public string Nombre { get; set; }
    public List<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();

    public void AgregarAsignatura(Asignatura asignatura)
    {
        Asignaturas.Add(asignatura);
    }
    public void ImprimirDiagramaGeneral()
    {
        Console.WriteLine("\n+--------------------+");
        Console.WriteLine("|      Programa      |");
        Console.WriteLine("+--------------------+");
        Console.WriteLine($"| Nombre: {Nombre}");
        Console.WriteLine("+--------------------+");

        foreach (var a in Asignaturas)
        {
            Console.WriteLine($"| - {a.Nombre} ({a.Codigo})");
        }

        Console.WriteLine("=======================================\n");
    }
}

