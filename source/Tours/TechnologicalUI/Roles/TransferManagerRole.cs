using System;
using System.Collections.Generic;
using Tours;
using Tours.ComponentsBL;

namespace TechnologicalUI
{
    public class TransferManagerRole : TechnologicalUI
    {
        protected TransferManagerController transferManager;
        protected Output outAll;
        public TransferManagerRole(TransferManagerController _transferManager, Output _outAll)
        {
            transferManager = _transferManager;
            outAll = _outAll;
        }

        public void Play()
        {
            while (true)
            {
                Console.WriteLine("0 - Конец роли менеджера трансфера\n" +
                    "1 - Показать весь трансфер\n" +
                    "2 - Добавить трансфер\n" +
                    "3 - Изменить трансфер\n" +
                    "4 - Удалить трансфер\n" +
                    "5 - Добавить автобус\n" +
                    "6 - Изменить автобус\n" +
                    "7 - Удалить автобус\n");

                string testStr = Console.ReadLine();
                int test = Convert.ToInt32(testStr);

                if (test == 0)
                {
                    break;
                }

                switch (test)
                {
                    case 1:
                        GetAllTransfer();
                        break;
                    case 2:
                        AddTransfer();
                        break;
                    case 3:
                        UpdateTransfer();
                        break;
                    case 4:
                        DeleteTransfer();
                        break;
                    case 5:
                        AddBus();
                        break;
                    case 6:
                        UpdateBus();
                        break;
                    case 7:
                        DeleteBus();
                        break;
                    default:
                        break;
                }
            }
        }

        void GetAllTransfer()
        {
            List<Transfer> transfer = transferManager.GetAllTransfer();
            for (int i = 0; i < transfer.Count; i++)
            {
                Transfer tran = transfer[i];
                outAll.outputTransfer(tran);
            }
        }

        void AddTransfer()
        {
            Transfer tran = new Transfer { Transferid = 11, Planeticket = 4, Trainticket = 7, Busticket = 3};
            transferManager.AddTransfer(tran);

            List<Transfer> transfer = transferManager.GetAllTransfer();
            for (int i = 0; i < transfer.Count; i++)
            {
                Transfer ctran = transfer[i];
                outAll.outputTransfer(ctran);
            }
        }

        void UpdateTransfer()
        {
            Transfer ntran = new Transfer { Transferid = 11, Planeticket = 23, Trainticket = 7, Busticket = 3 };
            transferManager.UpdateTransfer(ntran);

            List<Transfer> transfer = transferManager.GetAllTransfer();
            for (int i = 0; i < transfer.Count; i++)
            {
                Transfer ctran = transfer[i];
                outAll.outputTransfer(ctran);
            }
        }

        void DeleteTransfer()
        {
            transferManager.DeleteTransferByID(11);

            List<Transfer> transfer = transferManager.GetAllTransfer();
            for (int i = 0; i < transfer.Count; i++)
            {
                Transfer ctran = transfer[i];
                outAll.outputTransfer(ctran);
            }
        }

        void AddBus()
        {
            DateTime dateB = new DateTime(2022, 03, 10, 15, 40, 00);
            DateTime dateE = new DateTime(2022, 03, 10, 17, 24, 00);
            Busticket bus = new Busticket { Bustid = 11, Bus = 4, Seat = 7, Cityfrom = 2, Cityto = 3, Departuretime = dateB.TimeOfDay, Arrivaltime = dateE.TimeOfDay, Luggage = false, Cost = 2300};
            transferManager.AddBus(bus);

            List<Busticket> buses = transferManager.GetAllBuses();
            for (int i = 0; i < buses.Count; i++)
            {
                Busticket cbus = buses[i];
                outAll.outputBus(cbus);
            }
        }

        void UpdateBus()
        {
            DateTime dateB = new DateTime(2022, 03, 10, 15, 40, 00);
            DateTime dateE = new DateTime(2022, 03, 10, 17, 24, 00);
            Busticket bus = new Busticket { Bustid = 11, Bus = 5, Seat = 7, Cityfrom = 2, Cityto = 3, Departuretime = dateB.TimeOfDay, Arrivaltime = dateE.TimeOfDay, Luggage = false, Cost = 2300 };
            transferManager.UpdateBus(bus);

            List<Busticket> buses = transferManager.GetAllBuses();
            for (int i = 0; i < buses.Count; i++)
            {
                Busticket cbus = buses[i];
                outAll.outputBus(cbus);
            }
        }

        void DeleteBus()
        {
            transferManager.DeleteBusByID(11);

            List<Busticket> buses = transferManager.GetAllBuses();
            for (int i = 0; i < buses.Count; i++)
            {
                Busticket cbus = buses[i];
                outAll.outputBus(cbus);
            }
        }
    }
}
