using MauiApp1.DB.Entities;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            var score = new Score
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(), // Это идентификатор пользователя
                Value = 120,
                Level = 2,
                Date = DateTime.Now,
                IncorrectAnswer = new List<IncorrectAnswer>
    {
        new IncorrectAnswer
        {
            Id = Guid.NewGuid(),
            Problem = "5 + 3",
            CorrectAnswer = 8,
            UserAnswer = 6
        }
    }
            };

            App.Database.AddScore(score);
            Test();

        }
        public async void Test()
        {

            var allScores = App.Database.GetAllScores();
            foreach (var score in allScores)
            {
                CounterBtn.Text = $"Очки: {score.Value}, Уровень: {score.Level}, Дата: {score.Date}";
                var incorrectAnswers = App.Database.GetIncorrectAnswers(score.Id);
                foreach (var answer in incorrectAnswers)
                {
                    CounterBtn.Text += $"Проблема: {answer.Problem}, Верный ответ: {answer.CorrectAnswer}, Ответ пользователя: {answer.UserAnswer}";
                }

            }


        }
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            await Navigation.PushModalAsync(new LoginPage());
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

    }

}
