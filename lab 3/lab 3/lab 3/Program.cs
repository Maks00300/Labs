using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Чтение входных данных
        string[] inputLines = File.ReadAllLines("INPUT.TXT");

        int N = int.Parse(inputLines[0]);
        int[] dv = inputLines[1].Split().Select(int.Parse).ToArray();
        int d = dv[0];
        int v = dv[1];
        int R = int.Parse(inputLines[2]);

        List<BusRoute> routes = new List<BusRoute>();

        for (int i = 3; i < R + 3; i++)
        {
            int[] routeInfo = inputLines[i].Split().Select(int.Parse).ToArray();
            routes.Add(new BusRoute(routeInfo[0], routeInfo[1], routeInfo[2], routeInfo[3]));
        }

        // Инициализация массива для хранения времени до каждой деревни
        int[] timeToVillage = new int[N + 1];
        for (int i = 0; i <= N; i++)
        {
            timeToVillage[i] = int.MaxValue;
        }

        // Алгоритм Дейкстры
        PriorityQueue<BusRoute> priorityQueue = new PriorityQueue<BusRoute>();
        timeToVillage[d] = 0;

        priorityQueue.Enqueue(new BusRoute(d, 0, d, 0));

        while (priorityQueue.Count > 0)
        {
            BusRoute currentRoute = priorityQueue.Dequeue();

            if (currentRoute.VillageTo == v)
            {
                File.WriteAllText("OUTPUT.TXT", currentRoute.ArrivalTime.ToString());
                return;
            }

            foreach (BusRoute nextRoute in routes.Where(r => r.VillageFrom == currentRoute.VillageTo && r.DepartureTime >= currentRoute.ArrivalTime))
            {
                int newTime = currentRoute.ArrivalTime + (nextRoute.DepartureTime - currentRoute.ArrivalTime) + (nextRoute.ArrivalTime - nextRoute.DepartureTime);

                if (newTime < timeToVillage[nextRoute.VillageTo])
                {
                    timeToVillage[nextRoute.VillageTo] = newTime;
                    priorityQueue.Enqueue(new BusRoute(nextRoute.VillageFrom, nextRoute.DepartureTime, nextRoute.VillageTo, newTime));
                }
            }
        }

        // Если не удалось добраться до деревни v
        File.WriteAllText("OUTPUT.TXT", "-1");
    }
}

class BusRoute : IComparable<BusRoute>
{
    public int VillageFrom { get; }
    public int DepartureTime { get; }
    public int VillageTo { get; }
    public int ArrivalTime { get; }

    public BusRoute(int villageFrom, int departureTime, int villageTo, int arrivalTime)
    {
        VillageFrom = villageFrom;
        DepartureTime = departureTime;
        VillageTo = villageTo;
        ArrivalTime = arrivalTime;
    }

    public int CompareTo(BusRoute other)
    {
        return ArrivalTime.CompareTo(other.ArrivalTime);
    }
}

class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();

    public int Count => heap.Count;

    public void Enqueue(T item)
    {
        heap.Add(item);
        int i = Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;

            if (heap[parent].CompareTo(heap[i]) <= 0)
                break;

            Swap(parent, i);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty");

        T result = heap[0];
        int lastIndex = Count - 1;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);

        int i = 0;

        while (true)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int smallest = i;

            if (left < Count && heap[left].CompareTo(heap[smallest]) < 0)
                smallest = left;

            if (right < Count && heap[right].CompareTo(heap[smallest]) < 0)
                smallest = right;

            if (smallest == i)
                break;

            Swap(i, smallest);
            i = smallest;
        }

        return result;
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}
