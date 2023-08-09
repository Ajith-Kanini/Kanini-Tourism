import React from 'react';
import { Button, Modal } from 'react-bootstrap'; // Make sure to have react-bootstrap installed

function MyBookings() {
  const [showModal, setShowModal] = React.useState(false);

  const handleCloseModal = () => {
    setShowModal(false);
  };

  const handleShowModal = () => {
    setShowModal(true);
  };

  return (
    <section className="" style={{ backgroundColor: '#35558a' }}>
      <div className="container ">
        <div className="row d-flex justify-content-center align-items-center h-100 text-center">
          <div className="col">
            <Button
              variant="dark"
              size="sm"
              onClick={handleShowModal}
            >
              <i className="fas fa-info me-2"></i> Get information
            </Button>

            <Modal
              show={showModal}
              onHide={handleCloseModal}
              centered
            >
              <Modal.Header closeButton />
              <Modal.Body className="text-start text-black p-4">
                <h5 className="modal-title text-uppercase mb-5">Johnatan Miller</h5>
                <h4 className="mb-5" style={{ color: '#35558a' }}>Thanks for your order</h4>
                <p className="mb-0" style={{ color: '#35558a' }}>Payment summary</p>
                <hr className="mt-2 mb-4" style={{ height: 0, backgroundColor: 'transparent', opacity: 0.75, borderTop: '2px dashed #9e9e9e' }} />

                <div className="d-flex justify-content-between">
                  <p className="fw-bold mb-0">Ether Chair(Qty:1)</p>
                  <p className="text-muted mb-0">$1750.00</p>
                </div>

                <div className="d-flex justify-content-between">
                  <p className="small mb-0">Shipping</p>
                  <p className="small mb-0">$175.00</p>
                </div>

                <div className="d-flex justify-content-between pb-1">
                  <p className="small">Tax</p>
                  <p className="small">$200.00</p>
                </div>

                <div className="d-flex justify-content-between">
                  <p className="fw-bold">Total</p>
                  <p className="fw-bold" style={{ color: '#35558a' }}>$2125.00</p>
                </div>

              </Modal.Body>
              <Modal.Footer className="d-flex justify-content-center border-top-0 py-4">
                <Button
                  variant="primary"
                  size="lg"
                  className="mb-1"
                  style={{ backgroundColor: '#35558a' }}
                >
                  Track your order
                </Button>
              </Modal.Footer>
            </Modal>

          </div>
        </div>
      </div>
    </section>
  );
}

export default MyBookings;
