import React, { useState } from "react";
import { Table, Button } from "react-bootstrap";

function TableGeneric(props) {
  return (
    <Table responsive>
      <thead>
        <tr>
          {props.headers.map((item, index) => (
            <th key={index}>{item.name}</th>
          ))}
          {props.actions.map((item, index) => (
            <th key={index}></th>
          ))}
        </tr>
      </thead>
      <tbody>
        {props.data.map((item, index) => (
          <tr key={index}>
            {props.headers.map((header, indexHeader) => (
              <td key={indexHeader}>{item[header.index]}</td>
            ))}
            {props.actions.map((button, indexButton) => (
              <td key={indexButton}>
                <Button
                  variant={button.class}
                  onClick={() => button.event(item.id)}
                >
                  {button.name}
                </Button>
              </td>
            ))}
          </tr>
        ))}
      </tbody>
    </Table>
  );
}

export default TableGeneric;
