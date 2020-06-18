import React, { useState } from "react";
import { Container, Row, Col, Toast } from "react-bootstrap";

function ToastGeneric(props) {
  return (
    <div
      aria-live="polite"
      aria-atomic="true"
      style={{
        position: "relative",
        minHeight: "100px"
      }}
    >
      <Toast
        style={{
          position: "fixed",
          top: "10%",
          right: "10%",
        }}
        onClose={() => props.onClose()}
      >
        <Toast.Header>
          <img src="holder.js/20x20?text=%20" className="rounded mr-2" alt="" />
          <strong className="mr-auto">{props.title}</strong>
        </Toast.Header>
        <Toast.Body>{props.message}</Toast.Body>
      </Toast>
    </div>
  );
}

export default ToastGeneric;
