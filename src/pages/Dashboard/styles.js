import styled from 'styled-components';
import { darken } from 'polished';

export const Container = styled.div`
  width: 90%;
  max-width: 900px;
  margin: 50px auto;

  header {
    display: flex;
    align-self: center;
    align-items: center;
    justify-content: space-between;

    button {
      display: flex;
      align-items: center;
      justify-content: space-around;

      border: 0;
      background: #4dbaf9;
      border-radius: 4px;

      padding: 10px;
      margin: 5px 0 0;
      height: 36px;

      transition: background 0.2s;

      span {
        font-weight: bold;
        color: #fff;
        font-size: 14px;
        margin: 0 10px;
        align-self: center;
      }

      &:hover {
        background: ${darken(0.03, '#4dbaf9')};
      }
    }

    strong {
      font-size: 32px;
      color: #003b63;
    }
  }

  ul {
    display: grid;
    grid-template-columns: 1fr;
    grid-gap: 10px;
    margin-top: 30px;
  }
`;

export const Compras = styled.li`
  display: flex;
  align-items: center;
  justify-content: space-between;

  background: #fff;
  border: 0;
  border-radius: 4px;
  height: 62px;
  padding: 0 15px;

  transition: background 0.2s;

  &:hover {
    background: rgba(255, 255, 255, 0.6);
  }
`;

export const ItemDescricao = styled.div`
  strong {
    color: #333;
    font-size: 18px;
    font-weight: bold;
  }

  span {
    display: flex;
    color: #333;
    font-size: 12px;
    padding-top: 4px;
  }
`;

export const ItemValor = styled.div`
  display: flex;

  div {
    text-align: right;
    margin-right: 10px;

    strong {
      display: block;
      color: #333;
      font-size: 18px;
      font-weight: bold;
    }

    span {
      color: #333;
      font-size: 12px;
      padding-top: 4px;
    }
  }

  button {
    background: none;
    border: none;
    margin-left: 10px;
    color: #003b63;

    transition: color 0.2s;

    &:hover {
      color: #cc2229;
    }
  }
`;
