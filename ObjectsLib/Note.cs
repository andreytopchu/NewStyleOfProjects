using System;

namespace ObjectsLib
{
    public class Note
    {
        public string Text { get; private set; }
        public int SerialNumber { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsCompleted { get; set; }

        public Note(string text, int serialNumber, DateTime date, bool isCompleted)
        {
            if (serialNumber > 0)
            {
                this.Text = text;
                this.SerialNumber = serialNumber;
                this.Date = date;
                this.IsCompleted = isCompleted;
            }
        }

        public override string ToString()
        {
            string statusText;
            if (IsCompleted) { statusText = "выполнена"; }
            else { statusText = "не выполнена"; }
            return string.Format("№ {0}  Текст: {1}  Дата выполнения: {2}  Статус: {3}",SerialNumber,Text,Date.ToShortDateString(),statusText);
        }
    }
}
