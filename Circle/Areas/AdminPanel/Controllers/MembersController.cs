using Circle.Areas.AdminPanel.VievModels;
using Circle.Data;
using Circle.Migrations;
using Circle.Models;
using Circle.Utilites;
using Circle.Utilites.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Circle.Areas.AdminPanel.Controllers

    
  {
        [Area("AdminPanel")]
        public class MembersController : Controller
        {
            private readonly AppDbContext _context;
            private readonly IWebHostEnvironment _env;

            public MembersController(AppDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }
            public async Task<IActionResult> Index()
            {
            List<GetVM> getVM = await _context.Memmbers
                .Where(p => !p.IsDeleted)
                .Select(p => new GetVM
                {
                    Id = p.Id,
                    Image = p.Images,
                    Name = p.Name,
                    Desc = p.Desc
                })
                .ToListAsync();
                return View(getVM);
            }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createMemberVM)
        {
            if (!ModelState.IsValid) return View(createMemberVM);

            if (!createMemberVM.Image.CheckType("image/"))
            {
                ModelState.AddModelError(nameof(createMemberVM.Image), "file type is incoorecct");
                return View(createMemberVM);
            }
            if (!createMemberVM.Image.CheckSize(FileSize.Mb, 2))
            {
                ModelState.AddModelError(nameof(createMemberVM.Image), "file size is incoorecct");
                return View(createMemberVM);
            }

            Memmbers memmber = new()
            {
                Images = await createMemberVM.Image.CreateFile(_env.WebRootPath, "images"),
                Name = createMemberVM.Name,
                Desc = createMemberVM.Description
            };

            await _context.Memmbers.AddAsync(memmber);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1)
            {
                return BadRequest();
            }

            Memmbers? memmbers = await _context.Memmbers.FirstOrDefaultAsync(t => t.Id == id);

            if (memmbers is null)
            {
                return NotFound();
            }

            UpdateVM updateMemberVM = new()
            {
                Image= memmbers.Images,
                Name = memmbers.Name,
                Desc = memmbers.Desc
            };

            return View(updateMemberVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateVM updatePeopleVM)
        {
            if (id is null || id < 1)
            {
                return BadRequest();
            }

            Memmbers? memmbers = await _context.Memmbers.FirstOrDefaultAsync(t => t.Id == id);

            if (memmbers is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatePeopleVM);
            }

            if (updatePeopleVM.Photo is not null)
            {
                if (!updatePeopleVM.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError(nameof(updatePeopleVM.Image), "file type is incoorecct");
                    return View(updatePeopleVM);
                }
                if (!updatePeopleVM.Photo.CheckSize(FileSize.Mb, 2))
                {
                    ModelState.AddModelError(nameof(updatePeopleVM.Image), "file size is incoorecct");
                    return View(updatePeopleVM);
                }
                updatePeopleVM.Image.DeleteFile(_env.WebRootPath, "images");
                updatePeopleVM.Image = await updatePeopleVM.Photo.CreateFile(_env.WebRootPath, "images");
            }

            memmbers.Name = updatePeopleVM.Name;
            memmbers.Desc = updatePeopleVM.Desc;
            memmbers.Images = updatePeopleVM.Image;

            _context.Memmbers.Update(memmbers);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1)
            {
                return BadRequest();
            }

            Memmbers? memmbers = await _context.Memmbers.FirstOrDefaultAsync(t => t.Id == id);

            if (memmbers is null)
            {
                return NotFound();
            }

            memmbers.Images.DeleteFile(_env.WebRootPath, "images");

            _context.Memmbers.Remove(memmbers);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id < 1)
            {
                return BadRequest();
            }

            Memmbers? memmbers = await _context.Memmbers.FirstOrDefaultAsync(t => t.Id == id);
            if (memmbers is null)
            {
                return NotFound();
            }

            return View(memmbers);
        }
    }


}
