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
    margin-bottom: 30px;

    strong {
      font-size: 32px;
      color: #003b63;
    }

    ul {
      display: flex;
      align-content: space-between;
    }
  }

  aside {
    color: #333;
    padding: 30px;
    background: #fff;
    border-radius: 4px;

    footer {
      margin-top: 30px;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
`;

export const Button = styled.button`
  display: flex;
  align-items: center;
  justify-content: space-around;

  border: 0;
  border-radius: 4px;
  background: ${props => (props.blue ? '#4dbaf9' : '#cc2229')};

  padding: 10px;
  margin: 5px 0 0 20px;
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
    background: ${props => darken(0.03, props.blue ? '#4dbaf9' : '#cc2229')};
  }
`;

export const Loading = styled.div`
  color: #003b63;
  font-size: 30px;
  font-weight: bold;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
`;

export const DetalheTable = styled.table`
  width: 100%;
  thead th {
    color: #999;
    text-align: left;
    padding: 12px;
  }
  tbody td {
    padding: 12px;
    border-bottom: 1px solid #eee;
  }
  img {
    height: 100px;
  }
  strong {
    color: #333;
    display: block;
  }
  span {
    display: block;
    margin-top: 5px;
    font-size: 18px;
    font-weight: bold;
  }
  div {
    display: flex;
    align-items: center;
    input {
      border: 1px solid #ddd;
      border-radius: 4px;
      color: #666;
      padding: 6px;
      width: 50px;
    }
  }
`;

export const Total = styled.div`
  display: flex;
  align-items: baseline;
  span {
    color: #999;
    font-weight: bold;
  }
  strong {
    font-size: 28px;
    margin-left: 5px;
  }
`;
