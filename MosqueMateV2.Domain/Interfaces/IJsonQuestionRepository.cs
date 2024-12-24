using MosqueMateV2.Domain.DTOs;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonQuestionRepository : IDisposable
    {
        public List<DTOQuestions> GetAllQuestions();
        public DTOQuestions GetQuestionById(int id);
        public DTOQuestions GetRandomQuestion();
        public int GetAllQuestionsCount();
    }
}
