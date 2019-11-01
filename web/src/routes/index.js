import React from 'react';
import { Switch } from 'react-router-dom';
import Route from './Route';

import SignIn from '~/pages/SignIn';
import SignUp from '~/pages/SignUp';

import Dashboard from '~/pages/Dashboard';
import Detalhes from '~/pages/Detalhes';
import NovoEditar from '~/pages/NovoEditar';

export default function Routes() {
  return (
    <Switch>
      <Route path="/" exact component={SignIn} />
      <Route path="/registro" component={SignUp} />

      <Route path="/dashboard" component={Dashboard} isPrivate />
      <Route path="/detalhes/:key" component={Detalhes} isPrivate />
      <Route path="/novo-editar" exact component={NovoEditar} isPrivate />
      <Route path="/novo-editar/:key" component={NovoEditar} isPrivate />
      <Route path="/" component={() => <h1>404</h1>} />
    </Switch>
  );
}
