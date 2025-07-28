using ClosedXML.Excel;
using Hien_mau.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace Hien_mau.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public ReportController(Hien_mauContext context)
        {
            _context = context;
        }

        [HttpGet("blood-statistics/excel")]
        public async Task<IActionResult> ExportBloodStatisticsExcel()
        {
            var bloodStats = await _context.BloodInventories
                .GroupBy(i => i.BloodGroup + " " + i.RhType)
                .Select(g => new { Group = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Kho máu");

            ws.Cell("A1").Value = "BÁO CÁO THỐNG KÊ KHO MÁU";
            ws.Range("A1:B1").Merge();
            ws.Range("A1:B1").Style.Font.Bold = true;
            ws.Range("A1:B1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("A1:B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range("A1:B1").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Cell("A3").Value = "Nhóm máu";
            ws.Cell("B3").Value = "Số lượng";
            ws.Range("A3:B3").Style.Font.Bold = true;
            ws.Range("A3:B3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range("A3:B3").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            int row = 4;
            int dataStart = row;
            foreach (var item in bloodStats)
            {
                ws.Cell($"A{row}").Value = item.Group;
                ws.Cell($"B{row}").Value = item.Quantity;
                row++;
            }
            int dataEnd = row - 1;
            ws.Range($"A{dataStart}:B{dataEnd}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{dataStart}:B{dataEnd}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Cell($"A{row}").Value = "Tổng cộng:";
            ws.Cell($"B{row}").FormulaA1 = $"SUM(B{dataStart}:B{dataEnd})";
            ws.Range($"A{row}:B{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:B{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:B{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"BaoCao_KhoMau_{DateTime.Now:dd-MM-yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        [HttpGet("admin-summary/excel")]
        public async Task<IActionResult> ExportAdminSummaryExcel()
        {
            var userRoles = await _context.Users
                .Include(u => u.Role)
                .GroupBy(u => u.Role.RoleName)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToListAsync();

            var contentTypes = await _context.Contents
                .GroupBy(c => c.ContentType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            var bloodRequesters = await _context.BloodRequests
                .Include(r => r.User).ThenInclude(u => u.Role)
                .GroupBy(r => r.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    UserName = g.Select(x => x.User.Name).FirstOrDefault(),
                    RoleName = g.Select(x => x.User.Role.RoleName).FirstOrDefault(),
                    RequestCount = g.Count()
                })
                .ToListAsync();

            var totalRequesters = bloodRequesters.Count;
            var unprocessedRequests = await _context.BloodRequests.Where(r => r.Status == 0).CountAsync();
            var processedRequests = await _context.BloodRequests.Where(r => r.Status != 0).CountAsync();

            var bloodDonors = await _context.Appointments
                 .Include(a => a.User).ThenInclude(u => u.Role)
                 .Where(a => (a.Process == 2 && a.Status == true) || a.Process == 3 || a.Process == 4)
                 .GroupBy(a => a.UserID)
                 .Select(g => new
                 {
                     UserId = g.Key,
                     UserName = g.Select(x => x.User.Name).FirstOrDefault(),
                     RoleName = g.Select(x => x.User.Role.RoleName).FirstOrDefault(),
                     DonationCount = g.Count()
                 })
                 .ToListAsync();

            var totalDonors = bloodDonors.Count;

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Admin tổng hợp");

            ws.Cell("A1").Value = "BÁO CÁO DÀNH CHO ADMIN";
            ws.Range("A1:D1").Merge().Style.Font.Bold = true;
            ws.Range("A1:D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("A1:D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range("A1:D1").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            int row = 3;

            // USERS
            ws.Cell($"A{row++}").Value = "Người dùng theo vai trò";
            ws.Range($"A{row - 1}:B{row - 1}").Merge().Style.Font.Bold = true;
            ws.Cell($"A{row}").Value = "Vai trò";
            ws.Cell($"B{row}").Value = "Số lượng";
            ws.Range($"A{row}:B{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:B{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:B{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row++;

            int userStart = row;
            foreach (var r in userRoles)
            {
                ws.Cell($"A{row}").Value = r.Role;
                ws.Cell($"B{row}").Value = r.Count;
                row++;
            }
            int userEnd = row - 1;
            ws.Range($"A{userStart}:B{userEnd}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{userStart}:B{userEnd}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Cell($"A{row}").Value = "Tổng cộng";
            ws.Cell($"B{row}").FormulaA1 = $"SUM(B{userStart}:B{userEnd})";
            ws.Range($"A{row}:B{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:B{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:B{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row += 2;

            // CONTENTS
            ws.Cell($"A{row++}").Value = "Bài viết theo loại nội dung";
            ws.Range($"A{row - 1}:B{row - 1}").Merge().Style.Font.Bold = true;
            ws.Cell($"A{row}").Value = "Loại nội dung";
            ws.Cell($"B{row}").Value = "Số lượng";
            ws.Range($"A{row}:B{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:B{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:B{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row++;

            int contentStart = row;
            foreach (var c in contentTypes)
            {
                ws.Cell($"A{row}").Value = c.Type;
                ws.Cell($"B{row}").Value = c.Count;
                row++;
            }
            int contentEnd = row - 1;
            ws.Range($"A{contentStart}:B{contentEnd}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{contentStart}:B{contentEnd}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Cell($"A{row}").Value = "Tổng cộng";
            ws.Cell($"B{row}").FormulaA1 = $"SUM(B{contentStart}:B{contentEnd})";
            ws.Range($"A{row}:B{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:B{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:B{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row += 2;

            // BLOOD REQUESTS
            ws.Cell($"A{row++}").Value = "THỐNG KÊ YÊU CẦU MÁU";
            ws.Range($"A{row - 1}:B{row - 1}").Merge().Style.Font.Bold = true;
            ws.Cell($"A{row}").Value = "Yêu cầu chưa xử lý";
            ws.Cell($"B{row++}").Value = unprocessedRequests;
            ws.Cell($"A{row}").Value = "Yêu cầu đã xử lý";
            ws.Cell($"B{row++}").Value = processedRequests;
            ws.Cell($"A{row}").Value = "Tổng người yêu cầu";
            ws.Cell($"B{row++}").Value = totalRequesters;
            ws.Range($"A{row - 1}:B{row - 1}").Style.Font.Bold = true;
            ws.Range($"A{row - 3}:B{row - 1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row - 3}:B{row - 1}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row += 2;

            // List of requester
            ws.Cell($"A{row++}").Value = "Danh sách người yêu cầu";
            ws.Range($"A{row - 1}:D{row - 1}").Merge().Style.Font.Bold = true;
            ws.Cell($"A{row}").Value = "User ID";
            ws.Cell($"B{row}").Value = "Tên";
            ws.Cell($"C{row}").Value = "Vai trò";
            ws.Cell($"D{row}").Value = "Số yêu cầu";
            ws.Range($"A{row}:D{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:D{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:D{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row++;

            int reqStart = row;
            foreach (var req in bloodRequesters)
            {
                ws.Cell($"A{row}").Value = req.UserId;
                ws.Cell($"B{row}").Value = req.UserName;
                ws.Cell($"C{row}").Value = req.RoleName;
                ws.Cell($"D{row}").Value = req.RequestCount;
                row++;
            }
            int reqEnd = row - 1;
            ws.Range($"A{reqStart}:D{reqEnd}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{reqStart}:D{reqEnd}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Cell($"A{row}").Value = "Tổng cộng";
            ws.Cell($"D{row}").FormulaA1 = $"SUM(D{reqStart}:D{reqEnd})";
            ws.Range($"A{row}:D{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:D{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:D{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row += 2;

            // DONORS
            ws.Cell($"A{row++}").Value = "THỐNG KÊ HIẾN MÁU";
            ws.Cell($"A{row - 1}").Style.Font.Bold = true;
            ws.Cell($"A{row}").Value = "Tổng người hiến máu";
            ws.Cell($"B{row}").Value = totalDonors;
            ws.Range($"A{row}:B{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:B{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:B{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row += 2;

            ws.Cell($"A{row++}").Value = "Danh sách người hiến";
            ws.Range($"A{row - 1}:D{row - 1}").Merge().Style.Font.Bold = true;
            ws.Cell($"A{row}").Value = "User ID";
            ws.Cell($"B{row}").Value = "Tên";
            ws.Cell($"C{row}").Value = "Vai trò";
            ws.Cell($"D{row}").Value = "Số lần hiến";
            ws.Range($"A{row}:D{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:D{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:D{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            row++;

            int donorStart = row;
            foreach (var don in bloodDonors)
            {
                ws.Cell($"A{row}").Value = don.UserId;
                ws.Cell($"B{row}").Value = don.UserName;
                ws.Cell($"C{row}").Value = don.RoleName;
                ws.Cell($"D{row}").Value = don.DonationCount;
                row++;
            }
            int donorEnd = row - 1;
            ws.Range($"A{donorStart}:D{donorEnd}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{donorStart}:D{donorEnd}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Cell($"A{row}").Value = "Tổng cộng";
            ws.Cell($"D{row}").FormulaA1 = $"SUM(D{donorStart}:D{donorEnd})";
            ws.Range($"A{row}:D{row}").Style.Font.Bold = true;
            ws.Range($"A{row}:D{row}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range($"A{row}:D{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            ws.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"BaoCao_TongHop_Admin_{DateTime.Now:dd-MM-yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
