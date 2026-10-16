namespace Company.G01.PL.Service
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            var foldepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/files", folderName);
            var filename = $"{Guid.NewGuid}{file.FileName}";
            var filepath = Path.Combine(foldepath, filename);
            using var filestream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(filestream);
            return filename;
        }
        public static void DeleteFile(string folderName,string filename)
        {
            var fileepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/files", folderName,filename);
            if (File.Exists(fileepath)) 
                File.Delete(fileepath); 

        } 
        
    }
}
