using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class QuanLyServices : IQuanLyServices
    {
        

        public List<T> GetList<T>() where T : class
        {
            using(var _context = new AppDbContext())
            {
                try
                {
                    var data = _context.Set<T>().AsNoTracking().ToList();

                    var property = typeof(T).GetProperty("isDelete");
                    if (property != null)
                    {
                        return data.Where(t =>
                        {
                            var value = property.GetValue(t);
                            return value is bool isDelete && !isDelete;
                        }).ToList();
                    }

                    return data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving entities of type {typeof(T).Name}: {ex.Message}");
                    return new List<T>();
                }
            }
        }

        public T GetById<T>(params object[] keyValues) where T : class
        {
            using (var _context = new AppDbContext()) {
                try
                {
                    var entity = _context.Set<T>().Find(keyValues);
                    if (entity == null)
                    {
                        throw new Exception($"Không tìm thấy đối tượng {typeof(T).Name} với Id: {string.Join(",", keyValues)}");
                    }

                    var property = typeof(T).GetProperty("isDelete");
                    if (property != null)
                    {
                        var value = property.GetValue(entity);
                        if (value is bool isDelete && isDelete)
                        {
                            throw new Exception($"Đối tượng {typeof(T).Name} với Id: {string.Join(",", keyValues)} đã bị xóa mềm (isDelete = true)");
                        }
                    }
                    return entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving entity of type {typeof(T).Name} with ID {string.Join(",", keyValues)}: {ex.Message}");
                    return null;
                }
            }
                
        }

        public bool Add<T>(T entity) where T : class
        {
            using (var _context = new AppDbContext()) {
                try
                {
                    _context.Set<T>().Add(entity);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding entity of type {typeof(T).Name}: {ex.Message}");
                    return false;
                }
            }
                
        }

        public bool Update<T>(T entity) where T : class
        {
            using (var _context = new AppDbContext()) {
                try
                {
                    var entry = _context.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        _context.Set<T>().Attach(entity);
                    }

                    // Đánh dấu thực thể là đã thay đổi
                    entry.State = EntityState.Modified;

                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while updating entity of type {typeof(T).Name}: {ex.Message}");
                    return false;
                }
            }
                
        }

        public bool SoftDelete<T>(T entity) where T : class
        {
            using (var _context = new AppDbContext())
            {
                try
                {
                    var entry = _context.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        _context.Set<T>().Attach(entity);
                    }

                    var property = typeof(T).GetProperty("isDelete");
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(entity, true);
                        entry.State = EntityState.Modified;
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No 'isDelete' property found in entity.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while soft deleting entity of type {typeof(T).Name}: {ex.Message}");
                    return false;
                }
            }
                
        }

        public bool HardDelete<T>(T entity) where T : class
        {
            using (var _context = new AppDbContext())
            {
                try
                {
                    var entry = _context.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        _context.Set<T>().Attach(entity);
                    }

                    _context.Set<T>().Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deleting entity of type {typeof(T).Name}: {ex.Message}");
                    return false;
                }
            }
               
        }

        public string GenerateNewId<T>(string prefix, int totalLength) where T : class
        {
            using (var _context = new AppDbContext())
            {
                try
                {
                    var allEntities = _context.Set<T>().AsNoTracking().ToList();

                    var lastEntity = allEntities
                        .Select(e =>
                        {
                            var idProperty = e.GetType().GetProperty("id") ?? e.GetType().GetProperty("Id");
                            var id = idProperty?.GetValue(e) as string;
                            return new { Entity = e, Id = id };
                        })
                        .Where(x => x.Id != null && x.Id.StartsWith(prefix))
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefault();

                    int newNumericPart = 1;
                    if (lastEntity != null && !string.IsNullOrEmpty(lastEntity.Id))
                    {
                        var numericPart = lastEntity.Id.Substring(prefix.Length);
                        if (int.TryParse(numericPart, out int lastNumericPart))
                        {
                            newNumericPart = lastNumericPart + 1;
                        }
                    }

                    string newId = prefix + newNumericPart.ToString().PadLeft(totalLength - prefix.Length, '0');
                    return newId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while generating new ID for type {typeof(T).Name}: {ex.Message}");
                    return null;
                }
            }
        }

        public byte[] ConvertImageToByteArray(string filePath)
        {
            
            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllBytes(filePath);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while converting image to byte array: {ex.Message}");
                return null;
            }
        }

        public string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".svg":
                    return "image/svg+xml";
                default:
                    return "application/octet-stream";
            }
        }

        public string ConvertToBase64Image(byte[] bytes, string fileName)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            string contentType = GetContentType(fileName);
            string base64 = Convert.ToBase64String(bytes);

            return $"data:{contentType};base64,{base64}";
        }

        public string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
