import React, { useState, useEffect } from "react";
import TableGeneric from "../../components/TableGeneric";
import ModalGeneric from "../../components/ModalGeneric";
import { Container, Row, Col, Button } from "react-bootstrap";
import FormImport from "./FormImport";
import FormCreateCompany from "./FormCreateCompany";
import CompanyService from "../../services/CompanyService";

const api = new CompanyService();

function Companies(props) {
  const [modalImport, setModalImport] = useState({
    showModal: false,
    idCompany: null
  });

  const [showModalCreate, setShowModalCreate] = useState(false);
  const [data, setData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const result = await api.getCompanies();
      setData(result);
    };

    fetchData();
  }, [modalImport]);

  useEffect(() => {
    const fetchData = async () => {
      const result = await api.getCompanies();
      setData(result);
    };

    fetchData();
  }, [showModalCreate]);

  return (
    <Container className="mt-10">
      <Row>
        <Col>
          <h1>Companhias
          <Button variant="success" className="float-right" onClick={() => setShowModalCreate(true)}>Criar</Button>
          </h1>
        </Col>
      </Row>
      <TableGeneric
        actions={[
          {
            name: "Importação",
            event: id => setModalImport({ idCompany: id, showModal: true })
          }
        ]}
        data={data}
        headers={[
          {
            index: "id",
            name: "ID"
          },
          {
            index: "name",
            name: "Nome"
          },
          {
            index: "reliability",
            name: "Confiabilidade"
          }
        ]}
      />
      <ModalGeneric
        show={modalImport.showModal}
        title={"Importação"}
        handleClose={() => {
          setModalImport({ idCompany: null, showModal: false });
        }}
      >
        <FormImport
          idCompany={modalImport.idCompany}
          handleClose={() => {
            setModalImport({
              showModal: false,
              idCompany: null
            });
          }}
        />
      </ModalGeneric>

      <ModalGeneric
        show={showModalCreate}
        title={"Nova Empresa"}
        handleClose={() => {
          setShowModalCreate(false);
        }}
      >
        <FormCreateCompany
          handleClose={() => {
            setShowModalCreate(false);
          }}
        />
      </ModalGeneric>
    </Container>
  );
}

export default Companies;
