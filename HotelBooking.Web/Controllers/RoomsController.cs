using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HotelBooking.Core.Managers;
using HotelBooking.Web.Models;

namespace HotelBooking.Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomManager _roomManager;

        public RoomsController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        // GET: Rooms
        public IActionResult Index()
        {
            return View(_roomManager.GetAllRooms().Select(RoomViewModel.FromRoom).ToList());
        }

        // GET: Rooms/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room room = _roomManager.GetRoom(id.Value);
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

                _roomManager.TryCreateRoom(r, out var _);
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

            Room room = _roomManager.GetRoom(id.Value);
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
                    _roomManager.TryUpdateRoom(r, out var _);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_roomManager.GetRoom(room.Id) == null)
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

            Room room = _roomManager.GetRoom(id.Value);
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
            _roomManager.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
