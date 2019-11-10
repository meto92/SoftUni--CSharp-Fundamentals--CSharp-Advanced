using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class DepartmentRooms
{
    const int RoomsCount = 20;
    const int BedsInRoomCount = 3;

    private int firstFreeRoomNumber;
    private int firstFreeBedNumberInCurrentRoom;

    private string[,] rooms;

    public DepartmentRooms()
    {
        rooms = new string[RoomsCount, BedsInRoomCount];
        firstFreeRoomNumber = 0;
        firstFreeBedNumberInCurrentRoom = 0;
    }
    
    public void TryAddPatient(string patientName)
    {
        if (firstFreeRoomNumber == RoomsCount)
        {
            return;
        }

        rooms[firstFreeRoomNumber, firstFreeBedNumberInCurrentRoom++] = patientName;

        if (firstFreeBedNumberInCurrentRoom == 3)
        {
            firstFreeRoomNumber++;
            firstFreeBedNumberInCurrentRoom = 0;
        }
    }

    public void PrintPatients()
    {
        for (int roomNumber = 0; roomNumber < RoomsCount; roomNumber++)
        {
            for (int bedNumber = 0; bedNumber < BedsInRoomCount; bedNumber++)
            {
                if (rooms[roomNumber, bedNumber] == null)
                {
                    return;
                }

                Console.WriteLine(rooms[roomNumber, bedNumber]);
            }
        }
    }

    public void PrintPatientsInRoom(int roomNumber)
    {
        List<string> patientsInCurrentRoom = new List<string>();

        for (int bedNumber = 0; bedNumber < BedsInRoomCount; bedNumber++)
        {
            if (rooms[roomNumber - 1, bedNumber] == null)
            {
                break;
            }

            patientsInCurrentRoom.Add(rooms[roomNumber - 1, bedNumber]);
        }

        if (patientsInCurrentRoom.Count > 0)
        {
            Console.WriteLine(string.Join(Environment.NewLine, patientsInCurrentRoom.OrderBy(p => p)));
        }
    }
}

class Hospital
{
    static void Main(string[] args)
    {
        Dictionary<string, List<string>> patientsByDoctor = 
            new Dictionary<string, List<string>>();
        Dictionary<string, DepartmentRooms> roomsByDepartments = 
            new Dictionary<string, DepartmentRooms>();
        Regex pattern = new Regex(@"(\S+)\s+(\S+)\s+(\S+)\s+(\S+)");
        string input = null;

        while ((input = Console.ReadLine()) != "Output")
        {
            Match match = pattern.Match(input);

            string department = match.Groups[1].Value;
            string doctor = $"{match.Groups[2].Value} {match.Groups[3].Value}";
            string patient = match.Groups[4].Value;

            if (!roomsByDepartments.ContainsKey(department))
            {
                roomsByDepartments[department] = new DepartmentRooms();
            }

            roomsByDepartments[department].TryAddPatient(patient);

            if (!patientsByDoctor.ContainsKey(doctor))
            {
                patientsByDoctor[doctor] = new List<string>();
            }

            patientsByDoctor[doctor].Add(patient);
        }

        string command = null;

        while ((command = Console.ReadLine().Trim()) != "End")
        {
            if (Regex.IsMatch(command, "^\\S+$"))
            {
                string department = command;

                if (roomsByDepartments.ContainsKey(department))
                {
                    roomsByDepartments[department].PrintPatients();
                }
            }
            else if (Regex.IsMatch(command, "^\\S+ \\d+$"))
            {
                string[] inputParams = command.Split(' ');
                string department = inputParams[0];
                int roomNumber = int.Parse(inputParams[1]);

                if (roomsByDepartments.ContainsKey(department))
                {
                    roomsByDepartments[department].PrintPatientsInRoom(roomNumber);
                }
            }
            else
            {
                string doctor = command;

                if (patientsByDoctor.ContainsKey(doctor))
                {
                    Console.WriteLine(string.Join(Environment.NewLine, patientsByDoctor[doctor].OrderBy(p => p)));
                }
            }
        }
    }
}