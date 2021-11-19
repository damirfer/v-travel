using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Service
{
    public interface IHelperService
    {
        string UploadFiles();
        string GetImageUrl(string fileName);
    }

    public class HelperService:IHelperService
    {
        private readonly IHostingEnvironment _environment;
        private readonly IHttpContextAccessor _httpContext;
        public HelperService(IHostingEnvironment environment, IHttpContextAccessor httpContext)
        {
            _environment = environment;
            _httpContext = httpContext;
        }

        public string UploadFiles()
        {
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "images");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }
            var files = _httpContext.HttpContext.Request.Form.Files;
            foreach (var file in files)

            {
                if (file == null || file.Length == 0)
                {
                    continue;
                }

                var filePath = Path.Combine(uploadsRootFolder, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return file.FileName;
                }
            }

            return string.Empty;

        }

        public string GetImageUrl(string fileName)
        {
            var uploadsRootFolder = "https://fittraveldemo.azurewebsites.net/images/";
            string filePath = Path.Combine(uploadsRootFolder, fileName);

            return filePath;
        }


        public static class PasswordGenerator
        {
            /// <summary>
            /// Generates a Random Password
            /// respecting the given strength requirements.
            /// </summary>
            /// <param name="opts">A valid PasswordOptions object
            /// containing the password strength requirements.</param>
            /// <returns>A random password</returns>
            public static string Generate(
                int requiredLength = 8,
                int requiredUniqueChars = 4,
                bool requireDigit = true,
                bool requireLowercase = true,
                bool requireNonAlphanumeric = true,
                bool requireUppercase = true)
            {
                string[] randomChars = new[] {
                    "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                    "abcdefghijkmnopqrstuvwxyz",    // lowercase
                    "0123456789",                   // digits
                    "!@$?_-"                        // non-alphanumeric
                    };
                Random rand = new Random(Environment.TickCount);
                List<char> chars = new List<char>();

                if (requireUppercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[0][rand.Next(0, randomChars[0].Length)]);

                if (requireLowercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[1][rand.Next(0, randomChars[1].Length)]);

                if (requireDigit)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[2][rand.Next(0, randomChars[2].Length)]);

                if (requireNonAlphanumeric)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[3][rand.Next(0, randomChars[3].Length)]);

                for (int i = chars.Count; i < requiredLength
                    || chars.Distinct().Count() < requiredUniqueChars; i++)
                {
                    string rcs = randomChars[rand.Next(0, randomChars.Length)];
                    chars.Insert(rand.Next(0, chars.Count),
                        rcs[rand.Next(0, rcs.Length)]);
                }

                return new string(chars.ToArray());
            }
        }
        }
}
