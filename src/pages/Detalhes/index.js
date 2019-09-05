import React, { useState, useMemo, useEffect } from 'react';

import { toast } from 'react-toastify';

import { addMonths, format, parseISO } from 'date-fns';
import ptBR from 'date-fns/locale/pt-BR';
import { utcToZonedTime } from 'date-fns-tz';

import api from '~/services/api';
import history from '~/services/history';

import { MdEdit, MdDelete } from 'react-icons/md';

import { formatPrice } from '~/util/format';

import { Container, Button, Loading, Total, DetalheTable } from './styles';

export default function Detalhes({ match }) {
  const [loadingPage, setLoadingPage] = useState(true);
  const [loading, setLoading] = useState(false);
  const [compra, setCompra] = useState(null);
  const [parcelas, setParcelas] = useState(null);

  const key = useMemo(() => match.params.key, [match.params.key]);

  useEffect(() => {
    async function loadItem() {
      setLoadingPage(true);
      try {
        if (!key) {
          setCompra(null);
          setLoadingPage(false);
          return;
        }

        const response = await api.get(`compra/${key}`);

        let data = response.data;

        if (!data) {
          toast.warn('Compra não localizada.');
          history.push('/dashboard');
          return;
        }

        const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

        const dateFormatted = format(
          utcToZonedTime(parseISO(data.dataCompra), timezone),
          "d 'de' MMMM",
          {
            locale: ptBR,
          }
        );

        data = {
          ...data,
          dateFormatted,
          valorTotalFormated: formatPrice(data.valorTotal),
          valorParcela: formatPrice(data.valorTotal / data.quantidadeParcelas),
        };

        setCompra(data);
        setLoadingPage(false);
      } catch (err) {
        setLoadingPage(false);
        toast.error('Ops! Ocorreu um problema na hora de carregar os dados!');
        history.push('/dashboard');
      }
    }

    loadItem();
  }, [key]);

  useEffect(() => {
    async function loadParcelas() {
      if (!compra) return;

      const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

      var items = [];
      for (let i = 1; i <= compra.quantidadeParcelas; i++) {
        const dateFormatted = format(
          utcToZonedTime(addMonths(parseISO(compra.dataCompra), i), timezone),
          "dd'/'MM'/'yyyy",
          {
            locale: ptBR,
          }
        );

        items.push({
          numero: i,
          dataVencimento: dateFormatted,
          juros: compra.taxaJuros,
          valor: formatPrice(compra.valorTotal / compra.quantidadeParcelas),
        });
      }

      setParcelas(items);
    }

    loadParcelas();
  }, [compra]);

  async function handleDelete(key) {
    try {
      setLoading(true);

      await api.delete(`compra/${key}`);

      setLoading(false);

      toast.info('Registro excluído com sucesso!.');

      history.push('/dashboard');
    } catch {
      setLoading(false);
      toast.error('Falha na exclusão, verifique os dados informados!');
    }
  }

  if (loadingPage) {
    return <Loading>Aguarde...</Loading>;
  }

  return (
    <Container>
      <header>
        <strong>{compra.descricao}</strong>
        <ul>
          <li>
            <Button
              blue
              type="button"
              onClick={() => history.push(`/novo-editar/${compra.compraKey}`)}
            >
              <MdEdit size={18} color="#fff" /> <span>Editar</span>
            </Button>
          </li>
          <li>
            <Button
              type="button"
              onClick={() => handleDelete(compra.compraKey)}
            >
              {loading ? (
                <span>Aguarde...</span>
              ) : (
                <>
                  <MdDelete size={18} color="#fff" /> <span>Excluir</span>
                </>
              )}
            </Button>
          </li>
        </ul>
      </header>
      <aside>
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
            <strong>{compra.valorTotalFormated}</strong>
          </Total>
        </footer>
      </aside>
    </Container>
  );
}
