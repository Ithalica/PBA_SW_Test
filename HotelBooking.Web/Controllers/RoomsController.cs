using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HotelBooking.Web.Models;

namespace HotelBooking.Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRepository<Room> _repository;

        public RoomsController(IRepository<Room> repos)
        {
            _repository = repos;
        }

        // GET: Rooms
        public IActionResult Index()
        {
            return View(_repository.GetAll().Select(RoomViewModel.FromRoom).ToList());
        }

        // GET: Rooms/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room room = _repository.Get(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(RoomViewModel.FromRoom(room));
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description")] RoomInputModel room)
        {
            if (ModelState.IsValid)
            {
                var r = new Room(0)
                {
                    Description = room.Description
                };

                _repository.TryCreate(r, out var _);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room room = _repository.Get(id.Value);
            if (room == null)
            {
                return NotFound();
            }
            return View(RoomInputModel.FromRoom(room));
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description")] RoomInputModel room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var r = new Room(room.Id)
                {
                    Description = room.Description
                };

                try
                {
                    _repository.TryUpdate(r, out var _);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.Get(room.Id) == null)
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room room = _repository.Get(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(RoomViewModel.FromRoom(room));
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
