using System.Collections;

namespace WebApi.Test.InlineData;

public class CultureInlineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //yield devolve o valor para a classe invocadora, e depois continua a execução própria.
        //após a invocadora terminar, volta aqui e continua a execução, ou seja,
        //o próximo yield é chamado e devolve o próximo valor para a invocadora, e assim por diante.

        yield return new object[] { "en" };
        yield return new object[] { "fr" };
        yield return new object[] { "pt-BR" };
        yield return new object[] { "pt-PT" };
        
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
