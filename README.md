
# 📘 Sistema de Registro de Pensum en C# + Supabase

<img width="1024" height="1024" alt="image" src="https://github.com/user-attachments/assets/8e3495a7-5874-463f-bd31-a6d6cf5cfb3d" />    

Aplicación de consola desarrollada en **Visual Studio 2022 (.NET)** para registrar asignaturas del programa académico **Ingeniería de Sistemas**, aplicando principios de **Programación Orientada a Objetos** y almacenamiento en la nube con **Supabase**.

---

## 🎯 Objetivo

Permitir el ingreso de asignaturas desde consola, guardarlas en Supabase mediante API REST y mostrar un diagrama del pensum al finalizar.

---

## 🧠 Arquitectura del Sistema

Se incluyen **3 clases** principales:

| Clase | Rol | Relación |
|-------|-----|----------|
| Asignatura | Entidad principal | Se agrega al Programa |
| Profesor | Preparada para asignar docentes | Independiente por ahora |
| Programa | Representa todo el pensum | Contiene una lista de Asignaturas |

---

## 🛠️ Tecnologías

| Tecnología | Uso |
|-----------|-----|
| C# .NET Console | Desarrollo principal |
| Supabase | Almacenamiento en la nube |
| Programación Orientada a Objetos | Modelamiento del pensum |
| HttpClient REST + JSON | Comunicación con API |

---

## 📌 Requisitos Previos

✅ Visual Studio 2022 con .NET instalado  
✅ Acceso a **Supabase** con tabla configurada

### ✅ Crear la tabla en Supabase

Nombre de la tabla: **asignaturas**

| Campo | Tipo |
|-------|------|
| Codigo | text |
| Nombre | text |
| Prerrequisito | text |

Habilitar políticas **Insert** públicas o usar autenticación por API Key.

---

## ⚙️ Configurar la API REST

Modificar en el método `GuardarEnSupabase()`:

```
string url = "TU_URL_SUPABASE/rest/v1/asignaturas";
string apiKey = "TU_API_KEY";
```

Ejemplo de URL válida:

```
https://xxxxx.supabase.co/rest/v1/asignaturas
```

---

## ▶️ Ejecución del Programa

1. Abrir proyecto en Visual Studio
2. Configurar URL + API Key de Supabase
3. Ejecutar con Ctrl + F5
4. Seguir instrucciones en consola

---

## 🖥️ Código completo Program.cs

```csharp
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
        Console.WriteLine($"| Especialidad: {Especialidad}");
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
        Console.WriteLine("\n===== DIAGRAMA GENERAL DEL PENSUM =====");

        Console.WriteLine("\n+--------------------+");
        Console.WriteLine("|      Programa      |");
        Console.WriteLine("+--------------------+");
        Console.WriteLine($"| Nombre: {Nombre}");
        Console.WriteLine("+--------------------+");

        foreach (var a in Asignaturas)
        {
            Console.WriteLine($"   |----> {a.Nombre} ({a.Codigo})");
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

            Console.Write("¿Desea ingresar otra asignatura? (S/N): ");
            continuar = Console.ReadLine()?.Trim().ToUpper() == "S";
        }

        programa.ImprimirDiagramaGeneral();
        Console.WriteLine("Proceso finalizado");
        Console.WriteLine("Presiona Enter para salir...");
        Console.ReadLine();
    }

    static Asignatura RegistrarAsignatura()
    {
        Console.Write("\nCódigo asignatura: ");
        string codigo = Console.ReadLine();
        Console.Write("Nombre asignatura: ");
        string nombre = Console.ReadLine();
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
        string url = "TU_URL_SUPABASE/rest/v1/asignaturas";
        string apiKey = "TU_API_KEY";

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", apiKey);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

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
```

---

## 🚀 Mejoras futuras

✔ CRUD completo desde consola  
✔ Consulta de datos desde Supabase  
✔ Asignación de Profesor a Asignatura  
✔ Interfaz gráfica WinForms  
✔ Exportar diagrama a PDF  

---

## 🧑‍💻 Autor

Proyecto educativo orientado a fortalecer conceptos POO con backend en la nube.

📬 Para soporte o mejoras, puedes pedírmelas y avanzamos juntos.
