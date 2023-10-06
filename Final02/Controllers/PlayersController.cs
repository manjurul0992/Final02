using Final02.Models;
using Final02.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace Final02.Controllers
{
    public class PlayersController : Controller
    {
        private Final02Context _context;
        private IWebHostEnvironment _environment;

        public PlayersController(Final02Context context,IWebHostEnvironment environment)
        {
            this._context=context;
            this._environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Players.Include(x=>x.SeriesEntries).ThenInclude(y=>y.Format).ToListAsync());
        }
        public IActionResult AddNewFormats(int?id)
        {
            ViewBag.format = new SelectList(_context.Formats, "FormatId", "FormatName", id.ToString() ?? "");
            return PartialView("_addNewFormats");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerVM playerVM, int[] FormatId)
        {
            Player player = new Player
            {
                PlayerName = playerVM.PlayerName,
                DateOfBirth = playerVM.DateOfBirth,
                Phone=playerVM.Phone,
                MaritalStatus = playerVM.MaritalStatus
            };
            string webroot = _environment.WebRootPath;
            string picturefilename = Path.GetFileName(playerVM.PicturePath.FileName);
            string save=Path.Combine(webroot, picturefilename);
            using( var stream=new FileStream(save,FileMode.Create))
            {
                playerVM.PicturePath.CopyToAsync(stream);
                player.Picture = "/" + picturefilename;
            }
            foreach( var format in FormatId)
            {
                SeriesEntry seriesEntry = new SeriesEntry()
                {
                    Player=player,
                    PlayerId=player.PlayerId,
                    FormatId=format
                };
                _context.SeriesEntries.Add(seriesEntry);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int?id)
        {
            var player= await _context.Players.FirstOrDefaultAsync(x=>x.PlayerId==id);
            PlayerVM playerVM = new PlayerVM()
            {
                PlayerId=player.PlayerId,
                PlayerName=player.PlayerName,
                DateOfBirth=player.DateOfBirth,
                Phone=player.Phone,
                Picture=player.Picture,
                MaritalStatus=player.MaritalStatus
            };
            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach( var format in existFormat)
            {
                playerVM.FormatList.Add(format.FormatId);
            }
            
            return View(playerVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerVM playerVM, int[] FormatId)
        {

            Player player = new Player
            {
                PlayerId = playerVM.PlayerId,
                PlayerName = playerVM.PlayerName,
                DateOfBirth = playerVM.DateOfBirth,
                Phone = playerVM.Phone,
                MaritalStatus = playerVM.MaritalStatus,
                Picture = playerVM.Picture

            };
          


            string webroot = _environment.WebRootPath;
            string pictureFileName = Path.GetFileName(playerVM.PicturePath.FileName);
            string fileToSave = Path.Combine(webroot, pictureFileName);

            using (var stream = new FileStream(fileToSave, FileMode.Create))
            {
                playerVM.PicturePath.CopyTo(stream);
                player.Picture = "/" + pictureFileName;
            }


            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == player.PlayerId).ToList();
            foreach (var item in existFormat)
            {
                _context.SeriesEntries.Remove(item);
            }
            foreach (var item in FormatId)
            {
                SeriesEntry seriesEntry = new SeriesEntry()
                {

                    PlayerId = player.PlayerId,
                    FormatId = item
                };
                _context.SeriesEntries.Add(seriesEntry);

            }
            _context.Update(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Delete(int?id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach (var format in existFormat)
            {
                _context.SeriesEntries.Remove(format);
            }
            _context.Remove(player);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
