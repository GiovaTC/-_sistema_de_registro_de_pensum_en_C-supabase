using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

class Program
{
    static async Task Main()
    {
        var programa = new Programa { Nombre = "Ingeniería de Sistemas" };

        Console.WriteLine("=== Registro Pensum Sistemas ===");
        bool continuar = true;

        while (continuar)
        {
            var asignatura = RegistrarAsignatura();

            asignatura.ImprimirDiagrama();
            programa.AgregarAsignatura(asignatura);
            await GuardarEnSupabase(asignatura);

            Console.Write("¿Desea agregar otra asignatura? (s/n): ");
            continuar = Console.ReadLine()?.Trim().ToUpper() == "S";
        }

        programa.ImprimirDiagramaGeneral();
        Console.WriteLine("Proceso finalizado");
        Console.WriteLine("Presione ENTER para salir...");
        Console.ReadLine();
    }

    static Asignatura RegistrarAsignatura()
    {
        Console.Write("\nCódigo asignatura: ");
        string codigo = Console.ReadLine();
        Console.Write("Nombre asignatura: ");
        String nombre = Console.ReadLine();
        Console.Write("Prerrequisito: ");
        string prerrequisito = Console.ReadLine();

        return new Asignatura
        {
            Codigo = codigo,
            Nombre = nombre,
            Prerrequisito = prerrequisito
        };
    }

    static async Task GuardarEnSupabase(Asignatura asignatura)
    {
        string url = "https://wohhwcvrvwogmfnqrxwy.supabase.co/rest/v1/asignaturas";
        string apikey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6IndvaGh3Y3ZydndvZ21mbnFyeHd5Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjA5NzQzMDcsImV4cCI6MjA3NjU1MDMwN30.d-X1cA3PXaqAS5VnPWsTOLZEO16ajBIsrN_aggr9nQs";

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", apikey);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apikey}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(url, asignatura);

            if (response.IsSuccessStatusCode)

                Console.WriteLine("Guardado en Supabase ✔");
            else
                Console.WriteLine("Error Supabase ❌: " + await response.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Excepción al intentar guardar en Supabase: " + ex.Message);
        }
    }
}
