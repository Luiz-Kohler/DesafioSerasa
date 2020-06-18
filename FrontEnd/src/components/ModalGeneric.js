import React, { useState } from "react";
import { Button, Modal } from "react-bootstrap";

function ModalGeneric(props) {
  const { handleClose, show } = props;
  return (
    <>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{props.title}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{props.children}</Modal.Body>
      </Modal>
    </>
  );
}

export default ModalGeneric;
