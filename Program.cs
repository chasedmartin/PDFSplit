using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

class Program
{
    static void Main(string[] args)
    {
        // find all pdf documents in current folder
        var currentPath = Directory.GetCurrentDirectory();
        var pdfFiles = Directory.GetFiles(currentPath, "*.pdf");
            
        foreach(var file in pdfFiles){
            PdfDocument pdf = PdfReader.Open(file, PdfDocumentOpenMode.Import);

            string name = Path.GetFileNameWithoutExtension(file);
            // create containing folders for output
            System.IO.Directory.CreateDirectory(name);

            List<PdfDocument> output = new List<PdfDocument>();
            // split documents into separate pages
            for (int idx = 0; idx < pdf.PageCount; idx++)
            {
                // Create new document
                PdfDocument outputDocument = new PdfDocument();
                outputDocument.Version = pdf.Version;
                outputDocument.Info.Title = String.Format("Page {0} of {1}", idx + 1, pdf.Info.Title);
                outputDocument.Info.Creator = pdf.Info.Creator;

                // Add the page and save it
                outputDocument.AddPage(pdf.Pages[idx]);
                output.Add(outputDocument);
                outputDocument.Save(String.Format("./{0}/{0} - Page {1}.pdf", name, idx + 1));
            }
        }
    }
}