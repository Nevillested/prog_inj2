using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_inj2
{
    class Program
    {
        static List<Tuple<string, string, int>> array = new List<Tuple<string, string, int>>();
        static void copy()
        {
            //очистка файла
            using (var fs = new FileStream(@"C:\Users\Weabo\Desktop\qwe.txt", FileMode.Truncate))
            {
            }
            //запись в файл
            using (StreamWriter writer = File.AppendText(@"C:\Users\Weabo\Desktop\qwe.txt"))
                //writer.WriteLine(a + " " + b + " " + c);
                for (int i = 0; i < array.Count; i++)
                {
                    writer.WriteLine("код"+array[i].Item1+"адрес" + array[i].Item2+"количество" + array[i].Item3);
                }
        }
        static void add(ref string a, string b, int c)
        {
            array.Add(Tuple.Create(a, b, c)); // метод добавления в мой список записи из 3 значений
            Console.WriteLine("Добавлено в массив"); //пишет в консоль, шо все ок
            copy();
            Console.WriteLine("Добавлено в файл");
        } //функция добавления значений в массив и в файл
        static void delete() //это функция удаления, работаящая на 2 пункт меню
        {
            Console.WriteLine("Введите номер удаляемой записи");
            int del = int.Parse(Console.ReadLine()); //создаем переменную номера удаляемой записи и переводим ее в тип int
            del = del - 1; //постольку поскольку номер начинается с нуля, то вычитаем единицу
            if (del <= array.Count) //аррай.каунт-это длина массива в c#
            {
                array.Remove(array[del]);
                Console.WriteLine("Удалено из массива");
            }
            else
            {
                Console.WriteLine("Такой записи нет");
            }
            copy();
            Console.WriteLine("Удалено из файла");
        }
        static void show() //функция для 3 пункта меню, которая печатает весь массив
        {
            Console.WriteLine("Данные массива:\r\n");
            for (int i = 0; i < array.Count; i++) //цикл проходит по всему массиву
            {
                Console.WriteLine("Код: " + array[i].Item1 + "; адрес: " + array[i].Item2 + "; количество: " + array[i].Item3); //печатает весь массив. В классе List touple в яп C# итемам присваиваются имена. В моем случае у кода автоматически присвоилось имя Item1, у адреса-Item2, у количества-Item3
            }
            Console.WriteLine("Данные файла:\r\n");
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Weabo\Desktop\qwe.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
        static void filtr(ref string kod)
        {
            for (int i = 0; i < array.Count; i++) //проходим по всему массиву
            {
                if (array[i].Item1 == kod) // если такой код находит, то...
                {
                    Console.WriteLine("Код: " + array[i].Item1 + "; адрес: " + array[i].Item2 + "; количество: " + array[i].Item3); //...то печатает все три итема этой записи
                }
            }
        } //функция фильтрации
        static void summa(ref string code)
        {
            int summa = 0; //приравнивает сумму к нулю, ибо надо проинициализировать переменную
            for (int i = 0; i < array.Count; i++) //проходится по всему массиву в поиске записей с нашим кодом и...
            {
                if (array[i].Item1 == code)//...и если находит, то...
                {
                    summa = summa + array[i].Item3;//...то суммирует все это дело
                }
            }
            Console.WriteLine("Суммарное количество: " + summa); //печатает в консоль сумму
        } //функция суммарного кол-во по заданному коду
        static void sortirovka() //функия сортировки, для 6 пункта меню 
        {
            for (int i = 0; i < array.Count; i++)
            {
                for (int j = 0; j < array.Count - 1; j++)
                {
                    if (array[j].Item2.CompareTo(array[j + 1].Item2) > 0)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            copy();
            show();
        }
        static void Main(string[] args)
        {   //очистка файла
            //using (var fs = new FileStream(@"C:\Users\Weabo\Desktop\qwe.txt", FileMode.Truncate))
            //{
            //}
            while (true) //это условие на бесконечный цикл, чтобы всегда работало меню и можно было выбрать какой-либо пункт
            {
                Console.WriteLine("Меню:" + "\r\n1)добавление записей в массив (приемка)." + "\r\n2)удаление записей из массива (отгрузка)." + "\r\n3)вывод массива на экран." + "\r\n4)фильтрация массива по заданному пользователем коду детали." + "\r\n5)поиск суммарного количества по заданному коду детали." + "\r\n6)сортировка по полю «адрес ячейки»."); //меню

                var key = -1;
                try
                {
                    key = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Введите пунт меню!");
                }

                switch (key)
                {
                    case 1:
                        Console.WriteLine("Ведите через enter: код детали-адрес ячейки-количество в ячейке");
                        string a = Console.ReadLine(); //ловит код и помещает в "а"
                        string b = Console.ReadLine(); //ловит адресс и помещает в "б"
                        int c = int.Parse(Console.ReadLine()); //ловит количество и переводит из введенной "с" string в int
                        add(ref a, b, c);
                        break;
                    case 2:
                        delete();
                        break;
                    case 3:
                        show();
                        break;
                    case 4:
                        Console.WriteLine("Введите код детали");
                        string kod = Console.ReadLine(); //ловим введенный код детали
                        filtr(ref kod);
                        break;
                    case 5:
                        Console.WriteLine("Поиск суммарного количества по заданному коду. Введите код:");
                        string code = Console.ReadLine(); //ловит введенный код детали
                        summa(ref code);
                        break;
                    case 6:
                        sortirovka();
                        break;
                    default: break;
                }

                Console.WriteLine("\r\n"); //пишет в консоль, шо все ок
            }
        } //основная функция
    }
}