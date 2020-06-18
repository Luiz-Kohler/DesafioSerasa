import React, { useState } from "react";
import { Formik } from "formik";
import CompanyService from "../../services/CompanyService";
import { Row, Col } from "react-bootstrap";

const api = new CompanyService();

function FormImport(props) {
  const [formSubmitted, setFormSubmitted] = useState(false);

  if (formSubmitted)
    return (
      <>
        <Row>
          <Col>
            <p className="text-center">Importação realizada com sucesso.</p>

          </Col>
        </Row>
        <Row>
          <Col>
            <button type="submit" className="btn btn-primary float-right" onClick={() => props.handleClose()}>
              Ok!
            </button>
          </Col>
        </Row>
      </>
    );

  return (
    <Formik
      initialValues={{ idCompany: props.idCompany }}
      onSubmit={async (values, actions) => {
        await api.importFileCompany(values);
        setFormSubmitted(true);
      }}
      render={({ values, handleSubmit, setFieldValue }) => {
        return (
          <form onSubmit={handleSubmit}>
            <p>
              Necessário ser um arquivo <b>CSV</b>.
            </p>
            <div className="form-group">
              <label for="file">Selecione o arquivo:</label>
              <input
                id="file"
                name="file"
                type="file"
                onChange={event => {
                  setFieldValue("file", event.currentTarget.files[0]);
                }}
                required={true}
                className="form-control"
                accept=".csv"
              />
            </div>
            <button type="submit" className="btn btn-primary float-right">
              Enviar
            </button>
          </form>
        );
      }}
    />
  );
}

export default FormImport;
