using System;

public class Class1
{
    public Class1()
    {

        public IQueryable<OptionDto> GetOption()
        {
            var option = from b in db.Option
                           select new OptionDto()
                           {
                               Id = b.Id,
                               Text = b.Text,
                               Order = b.Order
                               QuestionId = b.QuestionId,
                           };

            return option;
        }


        [ResponseType(typeof(OptionDto))]
        public async Task<IHttpActionResult> GetOption(int id)
        {
            var option = await db.Option.Select(b =>
                new OptionDto()
                {
                    Id = b.Id,
                    Text = b.Text,
                    Order = b.Order
                    QuestionId = b.QuestionId,

                }).SingleOrDefaultAsync(b => b.Id == id);
            if (option == null)
            {
                return NotFound();
            }

            return Ok(option);
        }

        [ResponseType(typeof(OptionDto))]
        public async Task<IHttpActionResult> PostOption(Option option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Option.Add(option);
            await db.SaveChangesAsync();
            db.Entry(option).Reference().Load();

            var dto = new OptionDto()
            {
                Id = option.Id,
                Text = option.Text
                Order = option.Order
                QuestionId = b.QuestionId,
            };

            return CreatedAtRoute("DefaultApi", new { id = option.Id }, dto);
        }
    }
}
