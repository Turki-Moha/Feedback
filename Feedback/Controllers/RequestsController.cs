using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Feedback.Models;
using Microsoft.AspNetCore.Http;
using Feedback.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Feedback.Controllers
{
    public class RequestsController : Controller
    {
        private readonly FeedBackDB _context;

        public RequestsController(FeedBackDB context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Requests
        public async Task<IActionResult> Index()
        {
            var test = await _context.Requests.Include(x => x.User).ToListAsync();
            return View(test);
        }

		public async Task<IActionResult> Index1()
		{
            RequestViewModel model = new RequestViewModel();
            model.Requests = await _context.Requests.Include(x => x.User).ToListAsync();
			ViewBag.UserId = new SelectList(_context.Users.Where(item => item.Status == 2), "Id", "Name");
			return View(model);
		}

		// GET: Requests/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        public async Task<IActionResult> Show(Guid id)
        {
            if(id == Guid.Empty)
            {
                return RedirectToAction("Error", "Home");
			}
            var data = await _context.Requests.Include(x => x.User).FirstOrDefaultAsync(x => x.Gid == id);
            if(data == null)
            {
				return RedirectToAction("Error", "Home");
            }
            return View(data);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewBag.UserId = new SelectList(_context.Users.Where(item => item.Status == 2), "Id", "Name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Description,Status,CreatedDate")] Request request, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if(ImageUrl != null)
                {
                    if(ImageUrl.ContentType != "image/jpeg" && ImageUrl.ContentType != "image/png" && ImageUrl.ContentType != "image/jpg")
                    {
                        ModelState.AddModelError("ImageUrl", "Only jpeg and png files are allowed");
                        ViewBag.UserId = new SelectList(_context.Users.Where(item => item.Status == 2), "Id", "Name");
                        return View(request);
                    }

                    var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageUrl.FileName);
                    var filePath = System.IO.Path.Combine("wwwroot", "userImage", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }
                    request.ImageUrl = filePath;
                }

                request.Gid = Guid.NewGuid();
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
				ViewBag.UserId = new SelectList(_context.Users.Where(item => item.Status == 2), "Id", "Name");
				return PartialView("_IndexCreate");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", request.UserId);
            return View(request);
        }

        public async Task<IActionResult> EditRequest(Guid id)
        {
            if(id == Guid.Empty)
            {
				return RedirectToAction("Error", "Home");
			}
            var data = await _context.Requests.FirstOrDefaultAsync(x => x.Gid == id);
            if(data == null)
            {
                return RedirectToAction("Error", "Home");
            }
			ViewBag.UserId = new SelectList(_context.Users.Where(item => item.Status == 2), "Id", "Name");
			return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditRequest(Request request)
        {
			
			if (ModelState.IsValid)
            {
				var data = await _context.Requests.FirstOrDefaultAsync(x => x.Gid == request.Gid);
				data.Title = request.Title;
				data.Description = request.Description;
				data.Gid = request.Gid;
				data.CreatedDate = request.CreatedDate;
				data.UserId = request.UserId;
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index1));
			}
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", request.UserId);
            ModelState.AddModelError("", "Error");
			return View(request);
		}

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", request.UserId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Title,Description,Status,CreatedDate")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", request.UserId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            var data = await _context.Requests.FirstOrDefaultAsync(x => x.Gid == id);
            if(data == null)
            {
                return RedirectToAction("Error", "Home");
            }
            _context.Requests.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index1));
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
