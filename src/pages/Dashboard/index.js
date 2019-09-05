import React, { useEffect, useState } from 'react';

import { format, parseISO } from 'date-fns';
import ptBR from 'date-fns/locale/pt-BR';
import { utcToZonedTime } from 'date-fns-tz';

import { MdAddShoppingCart, MdChevronRight } from 'react-icons/md';

import api from '~/services/api';
import history from '~/services/history';
import { formatPrice } from '~/util/format';

import { Container, Compras, ItemDescricao, ItemValor } from './styles';

export default function Dashboard() {
  const [compras, setCompras] = useState([]);

  useEffect(() => {
    async function loadOrganizing() {
      const response = await api.get('compra');

      const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

      const data = response.data.map(compra => {
        const dateFormatted = format(
          utcToZonedTime(parseISO(compra.dataCompra), timezone),
          "d 'de' MMMM",
          {
            locale: ptBR,
          }
        );

        return {
          ...compra,
          dateFormatted,
          valorTotalFormated: formatPrice(compra.valorTotal),
          valorParcela: formatPrice(
            compra.valorTotal / compra.quantidadeParcelas
          ),
        };
      });

      setCompras(data);
    }

    loadOrganizing();
  }, []);

  return (
    <Container>
      <header>
        <strong>Compras</strong>
        <button type="button" onClick={() => history.push('/novo-editar')}>
          <MdAddShoppingCart size={18} color="#fff" />{' '}
          <span>Simular Compra</span>
        </button>
      </header>

      <ul>
        {compras.map(compra => (
          <Compras key={compra.compraKey}>
            <ItemDescricao>
              <strong>{compra.descricao}</strong>
              <span>{compra.dateFormatted}</span>
            </ItemDescricao>

            <ItemValor>
              <div>
                <strong>{compra.valorTotalFormated}</strong>
                <span>
                  {`${compra.quantidadeParcelas}x ${compra.valorParcela}`}
                </span>
              </div>
              <button
                type="button"
                onClick={() => history.push(`/detalhes/${compra.compraKey}`)}
              >
                <MdChevronRight size={20} />
              </button>
            </ItemValor>
          </Compras>
        ))}
      </ul>
    </Container>
  );
}
