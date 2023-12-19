// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Collections.Generic;

Graph<int> graph = new Graph<int>();

graph.AddEdge(1, 2);
graph.AddEdge(1, 3);
graph.AddEdge(2, 4);
graph.AddEdge(3, 4);
graph.AddEdge(4, 5);

Console.WriteLine("Graph adjacency list:");
graph.Display();








//Бинарный поиск делит отсортированный список пополам, если искомое меньше средьнего значение ищет по среднему значению в большей половине и так далее, если меньше то тоже самое но в меньшей
//Эффективность O(log n)
int BinarySearch(int[] arr, int item)
{
	int low = 0;
	int high = arr.Length - 1;
	int mid;
	int guess;
	while (low <= high)
	{
		mid = (low + high) / 2;
		guess= arr[mid];
		if (guess == item)
		{
			return mid;
		}
		if (guess > item)
		{
			high = mid - 1;
		}
		else
		{
			low = mid + 1;
		}
	}
	return 0;
}


//Сортировка выбором
//Эффективность O(n^2)
//Ищем минимальный элемент, ставим его на первое место, странтуем новый поиск, но со второго места, и так пока элементы не законатся
int[] SelectSort(int[] intArray)
{
	int indx; //переменная для хранения индекса минимального элемента массива
	for (int i = 0; i < intArray.Length; i++) //проходим по массиву с начала и до конца
	{
		indx = i; //считаем, что минимальный элемент имеет текущий индекс 
		for (int j = i; j < intArray.Length; j++) //ищем минимальный элемент в неотсортированной части
		{
			if (intArray[j] < intArray[indx])
			{
				indx = j; //нашли в массиве число меньше, чем intArray[indx] - запоминаем его индекс в массиве
			}
		}
		if (intArray[indx] == intArray[i]) //если минимальный элемент равен текущему значению - ничего не меняем
			continue;
		//меняем местами минимальный элемент и первый в неотсортированной части
		int temp = intArray[i]; //временная переменная, чтобы не потерять значение intArray[i]
		intArray[i] = intArray[indx];
		intArray[indx] = temp;
	}
	return intArray;
}

//Разделяй и властвуй
//Рекурсивная функция sum для подсчёта суммы элементов списка
int Sum(List<int> intList)
{
	int sum = 0;
	if (intList.Count == 1)
	{
		return intList[0];
	}
	else
	{
		int firstElem = intList[0];
		intList.RemoveAt(0);
		sum = firstElem + Sum(intList);
	}
	return sum;
}

//Быстрая сортировка 
//O(n * log n)
//Берём любой опорный элемент в массиве и разделяем массив на две части, элементы больше опорного и меньше, 
//для каждой такой части применяем тот же алгоритм, после сумируем полученные части
//Лучшее объяснение -> https://www.youtube.com/watch?v=CkqJLPiCeFw&ab_channel=BasicSloth
int[] QuickSort(int[] array, int minIndex, int maxIndex)
{
	if (minIndex >= maxIndex)
	{
		return array;
	}

	int pivotIndex = GetPivotIndex(array, minIndex, maxIndex);

	QuickSort(array, minIndex, pivotIndex - 1);

	QuickSort(array, pivotIndex + 1, maxIndex);

	return array;
}

int GetPivotIndex(int[] array, int minIndex, int maxIndex)
{
	int pivot = minIndex - 1;

	for (int i = minIndex; i < maxIndex; i++)
	{ 
		if (array[i] < array[maxIndex])
		{
			pivot++;
			Swap(ref array[pivot], ref array[i]);
		}
	}

	pivot++;
	Swap(ref array[pivot], ref array[maxIndex]);

	return pivot;
}

void Swap(ref int leftItem, ref int rightItem)
{
	int temp = leftItem;

	leftItem = rightItem;

	rightItem = temp;
}


//Пробую реализовать граф версия с гугла
//Рёбра
//public class Edge
//{
//	public Vertex ConnectedVertex { get; }
//	public int EdgeWeight { get; } //Пока не понял зачем нужно

//	public Edge(Vertex connectedVertex)
//	{
//		ConnectedVertex = connectedVertex;
//	}

//	//public Edge(Vertex connectedVertex, int weight)
//	//{
//	//	ConnectedVertex = connectedVertex;
//	//	//EdgeWeight = weight;
//	//}
//}

////Вершины
//public class Vertex
//{
//	public string Name { get; }
//	public List<Edge> Edges { get; }

//	//Конструктор
//	public Vertex(string name, List<Edge> edges)
//	{
//		Name = name;
//		Edges = edges;
//	}

//	public void AddNewEdge(Edge edge)
//	{
//		Edges.Add(edge);
//	}

//	public void AddNewEdge(Vertex vertex/*, int edgeWeight*/)
//	{
//		AddNewEdge(new Edge(vertex/*, edgeWeight*/));
//	}
//}

////Сам граф, то есть список вершин, которые содержат список рёбер
//public class Graph
//{
//	public List<Vertex> Vertices { get; }

//	//Конструктор
//	public Graph(List<Vertex> vertices)
//	{
//		Vertices = vertices;
//	}

//	public void AddVertex(Vertex vertex)
//	{
//		Vertices.Add(vertex);
//	}

//	public Vertex FindVertex(string vertexName)
//	{
//		foreach (var v in Vertices)
//		{
//			if (v.Name.Equals(vertexName))
//			{
//				return v;
//			}
//		}

//		return null;
//	}

//	public void AddEdge(string firstName, string secondName, Edge edge)
//	{
//		var vertex1 = FindVertex(firstName);
//		var vertex2 = FindVertex(secondName);
//		if (vertex2 != null && vertex1 != null)
//		{
//			vertex1.AddNewEdge(vertex1);
//			vertex2.AddNewEdge(vertex2);
//		}	
//	}
//}

//Граф, better version
class Graph<T>
{
	//Словарь где первый элемент это вершина, второй список элементов с которыми она связана, по сути ребро
	private Dictionary<T, List<T>> adjacencyList;


	//Пустой конструктор
	public Graph()
	{
		adjacencyList = new Dictionary<T, List<T>>();
	}

	//Создаём вершину с пустым ребром
	public void AddVertex(T vertex)
	{
		adjacencyList[vertex] = new List<T>();
	}

	//Добавление рёбер, в параметрах первая вершина и вторая
	public void AddEdge(T source, T destination)
	{
		//Если указанных вершин нет, добавляем их
		if (!adjacencyList.ContainsKey(source))
			AddVertex(source);

		if (!adjacencyList.ContainsKey(destination))
			AddVertex(destination);

		//В рёбра(список соеденённых вершин) добавляем элементы в ребро source добавляем destonation и наоборот
		adjacencyList[source].Add(destination);
		adjacencyList[destination].Add(source); // Uncomment this line if the graph is directed
	}

	public void Display()
	{
		//Перебираем все вершины
		foreach (var vertex in adjacencyList)
		{
			Console.Write($"{vertex.Key}: ");
			//перебираем список соеденённых вершин(ребро)
			foreach (var neighbor in vertex.Value)
			{
				Console.Write($"{neighbor} ");
			}
			Console.WriteLine();
		}
	}
}

//class Program
//{
//	static void Main()
//	{
//		Graph<int> graph = new Graph<int>();

//		graph.AddEdge(1, 2);
//		graph.AddEdge(1, 3);
//		graph.AddEdge(2, 4);
//		graph.AddEdge(3, 4);
//		graph.AddEdge(4, 5);

//		Console.WriteLine("Graph adjacency list:");
//		graph.Display();
//	}
//}