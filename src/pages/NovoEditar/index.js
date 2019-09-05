import React, { useState, useMemo, useEffect } from 'react';
import { Form, Input } from '@rocketseat/unform';
import { toast } from 'react-toastify';
import * as Yup from 'yup';

import api from '~/services/api';
import history from '~/services/history';

import { MdAddCircleOutline } from 'react-icons/md';

import { formatPrice } from '~/util/format';

import { addMonths, format, parseISO } from 'date-fns';
import ptBR from 'date-fns/locale/pt-BR';
import { utcToZonedTime } from 'date-fns-tz';

import { Container, Loading, Total, DetalheTable } from './styles';

const schema = Yup.object().shape({
  descricao: Yup.string().required('A descrição é obrigatória!'),
  valor: Yup.number().required('O valor é obrigatório!'),
  taxaJuros: Yup.number().required('A taxa de juros é obrigatória!'),
  quantidadeParcelas: Yup.number().required(
    'A quantidade de parcelas é obrigatória!'
  ),
  dataCompra: Yup.date().required('A data é obrigatória!'),
});

export default function NovoEditar({ match }) {
  const [loadingPage, setLoadingPage] = useState(true);
  const [loading, setLoading] = useState(false);
  const [compra, setCompra] = useState(null);
  const [parcelas, setParcelas] = useState(null);

  const [valor, setValor] = useState();
  const [juros, setJuros] = useState();
  const [quantidade, setQuantidade] = useState();
  const [dataCompra, setDataCompra] = useState();
  const [valorTotal, setValorTotal] = useState();

  const key = useMemo(() => match.params.key, [match.params.key]);

  useEffect(() => {
    async function loadItem() {
      setLoadingPage(true);

      if (!key) {
        setCompra(null);
        setLoadingPage(false);
        return;
      }

      const response = await api.get(`compra/${key}`);

      const compra = response.data;

      if (!compra) {
        toast.warn('Compra não localizada.');
        history.push('/dashboard');
        return;
      }

      setCompra(compra);
      setLoadingPage(false);
    }

    loadItem();
  }, [key]);

  useEffect(() => {
    async function loadParcelas() {
      setParcelas([]);

      if (!valor || !juros || !quantidade || !dataCompra) return;

      const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

      var items = [];
      for (let i = 1; i <= quantidade; i++) {
        const dateFormatted = format(
          utcToZonedTime(addMonths(parseISO(dataCompra), i), timezone),
          "dd'/'MM'/'yyyy",
          {
            locale: ptBR,
          }
        );

        const total = valor * Math.pow(1 + juros / 100, quantidade);
        setValorTotal(formatPrice(total));

        items.push({
          numero: i,
          dataVencimento: dateFormatted,
          juros: juros,
          valor: formatPrice(total / quantidade),
        });
      }

      setParcelas(items);
    }

    loadParcelas();
  }, [valor, juros, quantidade, dataCompra]);

  async function handleAddSubmit(data) {
    try {
      setLoading(true);

      const total =
        data.valor *
        Math.pow(1 + data.taxaJuros / 100, data.quantidadeParcelas);

      data = {
        ...data,
        valorTotal: total,
      };

      await api.post('compra', data);

      setLoading(false);

      toast.info('Registro realizado com sucesso!.');

      history.push('/dashboard');
    } catch {
      setLoading(false);
      toast.error('Falha no cadastro, verifique os dados informados!');
    }
  }

  async function handleUpdateSubmit(data) {
    try {
      setLoading(true);

      console.tron.log(data);

      const response = await api.put(`compra/${key}`, data);

      console.tron.log(response);

      setLoading(false);

      toast.info('Registro atualizado com sucesso!.');

      history.push('/dashboard');
    } catch {
      setLoading(false);
      toast.error('Falha na atualização, verifique os dados informados!');
    }
  }

  if (loadingPage) {
    return <Loading>Aguarde...</Loading>;
  }

  return (
    <Container>
      <Form
        initialData={compra}
        schema={schema}
        onSubmit={compra ? handleUpdateSubmit : handleAddSubmit}
      >
        <Input name="descricao" placeholder="Descrição" />
        <Input
          name="valor"
          placeholder="Valor"
          value={valor}
          onChange={e => setValor(e.target.value)}
        />

        <Input
          name="taxaJuros"
          placeholder="Taxa de juros"
          value={juros}
          onChange={e => setJuros(e.target.value)}
        />

        <Input
          name="quantidadeParcelas"
          placeholder="Quantidade de parcelas"
          value={quantidade}
          onChange={e => setQuantidade(e.target.value)}
        />

        <Input
          type="date"
          name="dataCompra"
          placeholder="Data da compra"
          value={dataCompra}
          onChange={e => setDataCompra(e.target.value)}
        />

        {parcelas && (
          <DetalheTable>
            <thead>
              <tr>
                <th>Nº</th>
                <th>VENCIMENTO</th>
                <th>JUROS</th>
                <th>VALOR</th>
              </tr>
            </thead>
            <tbody>
              {parcelas.map(parcela => (
                <tr key={parcela.numero}>
                  <td>{parcela.numero}</td>
                  <td>{parcela.dataVencimento}</td>
                  <td>{parcela.juros}</td>
                  <td>{parcela.valor}</td>
                </tr>
              ))}
            </tbody>
          </DetalheTable>
        )}
        <footer>
          <Total>
            <span>TOTAL</span>
            <strong>{valorTotal}</strong>
          </Total>

          <button type="submit">
            {loading ? (
              <span>Aguarde...</span>
            ) : (
              <>
                <MdAddCircleOutline color={'#fff'} size={20} />
                <span>Salvar</span>
              </>
            )}
          </button>
        </footer>
      </Form>
    </Container>
  );
}
