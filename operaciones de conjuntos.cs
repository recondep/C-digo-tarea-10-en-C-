using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

class Ciudadano
{
    public string Nombre { get; set; }
    public bool Vacunado { get; set; }
    public string TipoVacuna { get; set; } // "Pfizer", "AstraZeneca", "Ambas" o "Ninguna"
}

class Program
{
    static void Main()
    {
        // Generar ciudadanos
        List<Ciudadano> ciudadanos = CrearCiudadanos(500, 75, 75);

        // Obtener listados
        var noVacunados = ciudadanos.Where(c => c.TipoVacuna == "Ninguna").ToList();
        var soloPfizer = ciudadanos.Where(c => c.TipoVacuna == "Pfizer").ToList();
        var soloAstraZeneca = ciudadanos.Where(c => c.TipoVacuna == "AstraZeneca").ToList();
        var ambosVacunados = ciudadanos.Where(c => c.TipoVacuna == "Ambas").ToList();

        // Generar reporte en PDF
        CrearReportePDF(noVacunados, soloPfizer, soloAstraZeneca, ambosVacunados);
    }

    static List<Ciudadano> CrearCiudadanos(int total, int pfizerCount, int astrazenecaCount)
    {
        List<Ciudadano> ciudadanos = new List<Ciudadano>();
        Random random = new Random();

        // Crear ciudadanos con nombres ficticios
        for (int i = 1; i <= total; i++)
        {
            ciudadanos.Add(new Ciudadano
            {
                Nombre = $"Ciudadano {i}",
                Vacunado = false,
                TipoVacuna = "Ninguna"
            });
        }

        // Vacunar a 75 ciudadanos con Pfizer
        for (int i = 0; i < pfizerCount; i++)
        {
            int index;
            do
            {
                index = random.Next(total);
            } while (ciudadanos[index].TipoVacuna != "Ninguna"); // Asegurarse de que no esté ya vacunado

            ciudadanos[index].TipoVacuna = "Pfizer";
        }

        // Vacunar a 75 ciudadanos con AstraZeneca
        for (int i = 0; i < astrazenecaCount; i++)
        {
            int index;
            do
            {
                index = random.Next(total);
            } while (ciudadanos[index].TipoVacuna != "Ninguna"); // Asegurarse de que no esté ya vacunado

            ciudadanos[index].TipoVacuna = "AstraZeneca";
        }

        // Opcional: Vacunar a algunos ciudadanos con ambas vacunas
        // Aquí puedes agregar lógica si deseas que algunos ciudadanos tengan ambas vacunas

        return ciudadanos;
    }

    static void CrearReportePDF(List<Ciudadano> noVacunados, List<Ciudadano> soloPfizer, List<Ciudadano> soloAstraZeneca, List<Ciudadano> ambosVacunados)
    {
        Document documento = new Document();
        string rutaArchivo = "Reporte_Vacunacion.pdf";
        PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));
        documento.Open();

        documento.Add(new Paragraph("Reporte de Vacunación COVID-19"));
        documento.Add(new Paragraph(" ")); // Espacio en blanco

        // Listado de no vacunados
        documento.Add(new Paragraph("Listado de ciudadanos que no se han vacunado:"));
        foreach (var ciudadano in noVacunados)
        {
            documento.Add(new Paragraph(ciudadano.Nombre));
        }

        documento.Add(new Paragraph(" ")); // Espacio en blanco

        // Listado de vacunados con Pfizer
        documento.Add(new Paragraph("Listado de ciudadanos que han recibido la vacuna de Pfizer:"));
        foreach (var ciudadano in soloPfizer)
        {
            documento.Add(new Paragraph(ciudadano.Nombre));
        }

        documento.Add(new Paragraph(" ")); // Espacio en blanco

        // Listado de vacunados con AstraZeneca
        documento.Add(new Paragraph("Listado de ciudadanos que han recibido la vacuna de AstraZeneca:"));
        foreach (var ciudadano in soloAstraZeneca)
        {
            documento.Add(new Paragraph(ciudadano.Nombre));
        }

        documento.Add(new Paragraph(" ")); // Espacio en blanco

        // Listado de vacunados con ambas vacunas
        documento.Add(new Paragraph("Listado de ciudadanos que han recibido ambas vacunas:"));
        foreach (var ciudadano in ambos
