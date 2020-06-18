import React from "react";
import ReactDOM from "react-dom";
import * as serviceWorker from "./serviceWorker";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Companies from "./views/Companies";
import NoMatch from "./views/NoMatch";
import Menu from "./components/Menu";
import "bootstrap/dist/css/bootstrap.min.css";
import "./assets/styles.css";

function App() {
  return (
    <Router>
      <Menu />
      <Switch>
        <Route exact path="/">
          <Companies />
        </Route>
        <Route path="*">
          <NoMatch />
        </Route>
      </Switch>
    </Router>
  );
}

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);

serviceWorker.unregister();
