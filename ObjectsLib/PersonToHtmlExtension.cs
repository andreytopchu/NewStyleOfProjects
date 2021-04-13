using System;
using System.IO;
using System.Text;

namespace ObjectsLib
{
   public static class PersonToHtmlExtension
    {
        public static void PrintPersonToHtml(this Person person)
        {
            var htmlPage =
                @"<!DOCTYPE html>
                <html lang=""en"">
                <head> 
	            <meta charset=""UTF - 8"">
                <meta name = ""viewport"" content = ""width=device-width, initial-scale=1.0"">
     
                    <title> Persons </title>
    

                <style>
                .container {
                     width: 100 %;
                     max - width: 1230px;
                     padding: 15px 15px;
                     margin: 0 auto;
                     display: flex;
                     flex - wrap: wrap;
                  }
                .container_item{
                     font - size: 14px;
                     width: 200px;
                     border: 1px solid #580;
	                 padding: 10px;
                  }
                </style>

                </head>
                <body>

                <div class=""container"">
		             <div class=""container_item"">
			             Фамилия:<br>
			             Имя:<br>
			             Отчество:<br>
			             Дата рождения:<br>
			             Место рождения:<br>
			             Номер паспорта:<br>
		            </div>
		            <div class=""container_item"">
			            " + person.SecondName + @"<br>
			            " + person.FirstName + @"<br>
			            " + person.Patronymic + @"<br>
			            " + person.DateOfBirth + @"<br>
			            " + person.PlaceOfBirth + @"<br>
			            " + person.NumberOfPassport + @"<br>
		            </div>
	            </div>
                </body>
                </html>";

            var path =" D:/АНДРЕИНО/универ/4 семестр/Практика C#/Projects/TwoLayersSolution/Tests/PersonsInHtml/Person" + person.GetHashCode() + ".html";
            using (FileStream fileStream =
                File.Open(path, FileMode.OpenOrCreate))
            {
                byte[] content = new UTF8Encoding(true).GetBytes(htmlPage);
                fileStream.Write(content, 0, content.Length);
                Console.WriteLine("Данные о человеке записаны в : "    + path);
            }
        }
    }
}
