![Template-Portifolio-Html5-Css3-Jquery-Bootstrap](https://github.com/jeftegoesdev/ProjetoLerAquivoExcel/blob/master/Images/ValidacaoExcel.png?raw=true)

## O que é esse projeto?

É um pequeno/simples projeto que importa arquivos excel (.xls, .xlsx), além de importar ele faz algumas validações pré-definidas:

- Todos os campos são obrigatórios;
- O campo data de entrega não pode ser menor ou igual que o dia atual;
- O campo descrição precisa ter o tamanho máximo de 50 caracteres;
- O campo quantidade tem que ser maior do que zero;
- O campo valor unitário deve ser maior que zero e suas casas decimais devem ser arredondadas matematicamente para duas casas decimais.

- Caso o lote seja válido: Os dados devem ser salvos em um banco de dados relacional
  (de sua escolha), que respeite o tipo de dados e suas validações, e deverá ser
  adicionado um identificador único para a importação (ID). O status de retorno deverá ser
  o 200 e os dados de retorno ficam a critério do desenvolvedor para facilitar a construção
  das demais partes.

- Em caso de erros de validação: a API deverá retornar o status 400 (bad request) com
  uma lista de erros, contendo o número da linha do arquivo de Excel e o erro, ou erros,
  de validação.

## Back-end

.Net Core

## Padrões utilizados

- Repository pattern

## Front-end

Angular 4+ (10)

## Bibliotecas utilizadas

- Exemplo dashboard https://getbootstrap.com/docs/4.5/examples/dashboard/
- ngx-bootstrap (Angular)
- EF Core
- EPPlus (ler arquivo excel)
