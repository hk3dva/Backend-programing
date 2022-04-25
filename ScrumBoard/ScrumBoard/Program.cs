using ScrumBoard;

Board wa = new Board("test Board");

wa.AddNewColumn("1");
wa.AddNewColumn("2");
wa.AddNewColumn("3");
wa.AddNewColumn("4");
wa.AddNewColumn("5");
wa.AddNewCard("писать тесты");
wa.AddNewCard("сделать дз");
wa.MoveCard(0, 1, 2);
Console.WriteLine(wa.GetColumn(0).GetTask(0).Title);
Console.WriteLine(wa.GetColumn(2).GetTask(0).Title);
wa.MoveCard(0, 0, 2);
Console.WriteLine(wa.GetColumn(0).GetTask(0).Title);
Console.WriteLine(wa.GetColumn(2).GetTask(0).Title);