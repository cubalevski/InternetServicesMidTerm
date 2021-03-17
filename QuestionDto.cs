using System;

public class Class1
{
	public Class1()
	{

        public IQueryable<QuestionsDto> GetQuestion()
        {
            var question = from b in db.Question
                        select new QuestionDto()
                        {
                            Id = b.Id,
                            Text = b.Text,
                            Description = b.Description
                        };

            return question;
        }


        [ResponseType(typeof(QuestionDto))]
        public async Task<IHttpActionResult> GetQuestion(int id)
        {
            var question = await db.Question.Select(b =>
                new QuestionDto()
                {
                    Id = b.Id,
                    Text = b.Text,
                    Description = b.Description,

                }).SingleOrDefaultAsync(b => b.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [ResponseType(typeof(QuestionDto))]
        public async Task<IHttpActionResult> PostQuestion(Questions question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Question.Add(question);
            await db.SaveChangesAsync();
            db.Entry(question).Reference().Load();

            var dto = new QuestionDto()
            {
                Id = question.Id,
                Text = question.Text,
                Description = question.Description
            };

            return CreatedAtRoute("DefaultApi", new { id = question.Id }, dto);
        }
    }
}
