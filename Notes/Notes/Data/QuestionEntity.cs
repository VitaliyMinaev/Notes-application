using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    public class QuestionEntity
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }

        public QuestionEntity()
        {
            QuestionText = "None";
            AnswerText = "None";
        }
        public QuestionEntity(string questionText, string answerText)
        {
            QuestionText = questionText;
            AnswerText = answerText;
        }
    }
}
