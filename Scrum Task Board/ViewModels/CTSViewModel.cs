using Scrum_Task_Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scrum_Task_Board.ViewModels
{
    public class CTSViewModel
    {
        public List<Card> Card { get; set; }
        public List<Task> Task { get; set; }
        public List<State> State { get; set; }
    }
}