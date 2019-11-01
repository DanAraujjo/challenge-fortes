import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { Form, Input } from '@rocketseat/unform';
import * as Yup from 'yup';

import logo from '~/assets/logo.gif';

import { signUpRequest } from '~/store/modules/auth/actions';

const schema = Yup.object().shape({
  nome: Yup.string().required('O nome é obrigatório!'),
  email: Yup.string()
    .email('Insira um e-mail válido!')
    .required('O e-mail é obrigatório!'),
  password: Yup.string()
    .min(6, 'A senha deve ter no minímo 6 caracteres!')
    .required('A senha é obrigatória!'),
});

export default function SignUp() {
  const dispatch = useDispatch();
  const loading = useSelector(state => state.auth.loading);

  function handleSubmit({ nome, email, password }) {
    dispatch(signUpRequest(nome, email, password));
  }

  return (
    <>
      <img src={logo} alt="Fortes" />

      <Form schema={schema} onSubmit={handleSubmit}>
        <Input name="nome" placeholder="Nome completo" />
        <Input name="email" type="email" placeholder="Digite seu e-mail" />
        <Input
          name="password"
          type="password"
          placeholder="Sua senha secreta"
        />

        <button type="submit">{loading ? 'Aguarde...' : 'Criar conta'}</button>
        <Link to="/">Já tenho login</Link>
      </Form>
    </>
  );
}
