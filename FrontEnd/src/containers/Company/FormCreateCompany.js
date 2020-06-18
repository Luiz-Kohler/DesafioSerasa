import React, { useState } from "react";
import { Formik } from "formik";
import CompanyService from "../../services/CompanyService";
import { Row,Col } from "react-bootstrap";

const api = new CompanyService();

function FormCreateCompany(props) {
  const [formSubmitted,setFormSubmitted] = useState(false);

  if (formSubmitted)
    return (
      <>
      <Row>
      <Col>
        <p className="text-center">Salvo com sucesso!</p>
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
      initialValues={{}}
      onSubmit={async (values, actions) => {
        await api.save(values);
        setFormSubmitted(true);
      }}
      render={({ values, handleSubmit, setFieldValue }) => {
        return (
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label for="name">Nome da empresa:</label>
              <input
                id="name"
                name="name"
                type="text"
                onChange={event => {
                  setFieldValue("name", event.target.value);
                }}
                required={true}
                className="form-control"
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

export default FormCreateCompany;
